using AutoMapper;
using ETechParking.Application.Dtos.Locations.Users;
using ETechParking.Application.Dtos.Shared;
using ETechParking.Application.Interfaces.Locations.Users;
using ETechParking.Application.Services.Abstraction;
using ETechParking.Common.Tokens.Interfaces;
using ETechParking.Domain.Enums.Locations.Shifts;
using ETechParking.Domain.Interfaces.Repositories.Locations.Roles;
using ETechParking.Domain.Interfaces.Repositories.Locations.Shifts;
using ETechParking.Domain.Interfaces.Repositories.Locations.Users;
using ETechParking.Domain.Interfaces.UnitOfWork;
using ETechParking.Domain.Models.Locations.Shifts;
using ETechParking.Domain.Models.Locations.Users;
using ETechParking.Domain.Models.Shared;
using Microsoft.AspNetCore.Identity;

namespace ETechParking.Application.Services.Locations.Users;

public class UserService(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    SignInManager<User> signInManager,
    UserManager<User> userManager,
    IRoleRepository roleRepository,
    ITokensService tokensService,
    IShiftRepository shiftRepository) :
    BaseService<User, UserDto, int>(userRepository, unitOfWork, mapper), IUserService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly SignInManager<User> _signInManager = signInManager;
    private readonly UserManager<User> _userManager = userManager;
    private readonly IRoleRepository _roleRepository = roleRepository;
    private readonly ITokensService _tokensService = tokensService;
    private readonly IShiftRepository _shiftRepository = shiftRepository;

    public async override Task<UserDto> CreateAsync(UserDto userDto)
    {
        var user = _mapper.Map<User>(userDto);

        user.IsFirstLogin = true;

        var userResult = await _userManager.CreateAsync(user, userDto.Password);

        if (!userResult.Succeeded)
            return default!;

        var role = await _roleRepository.GetAsync(userDto.RoleId) ?? throw new Exception("Role not found.");
        await _userManager.AddToRoleAsync(user, role.Name!);

        return userDto;
    }

    public async override Task<IEnumerable<UserDto>> GetAllAsync()
    {
        var users = await _userRepository.GetAllAsync(
            filter: default!,
            orderBy: default!,
            u => u.Role,
            u => u.Location);
        var usersDtos = _mapper.Map<IReadOnlyList<UserDto>>(users);

        return usersDtos;
    }

    public async override Task<IEnumerable<UserDto>> GetAllPaginatedAsync(PaginatedModelDto paginatedModelDto)
    {
        var paginatedModel = _mapper.Map<PaginatedModel>(paginatedModelDto);
        var users = await _userRepository.GetAllPaginatedAsync(
            paginatedModel: paginatedModel,
            filter: default!,
            orderBy: default!,
            u => u.Role,
            u => u.Location);
        var usersDtos = _mapper.Map<IReadOnlyList<UserDto>>(users);

        return usersDtos;
    }

    public async override Task<UserDto> Update(UserDto newUserDto)
    {
        var user = _mapper.Map<User>(newUserDto);
        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
            return default!;

        return newUserDto;
    }

    public async Task<LoggedInDto> LoginAsync(LoginDto model, bool isCashier)
    {
        var user = await AuthenticateUserAsync(model);

        if (user == null)
            return default!;

        var shift = isCashier ? await CreateShiftAsync(user, model.StartDateTime) : default!;

        if (shift == null && isCashier)
            return default!;

        return new LoggedInDto
        {
            IsFirstLogin = user.IsFirstLogin,
            StartDateTime = shift?.StartDateTime,
            UserId = user.Id,
            LocationId = user.LocationId,
            ShiftId = shift?.Id,
            RoleName = user.Role.Name!,
            Token = await GetToken(user)
        };
    }

    public async Task<bool> ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
    {
        var user = await _userManager.FindByNameAsync(resetPasswordDto.UserName);

        if (user == null)
            return false;

        user.IsFirstLogin = false;

        var userUpdated = await _userManager.UpdateAsync(user);

        if (!userUpdated.Succeeded)
            return false!;

        var result = await _userManager.ChangePasswordAsync(
            user,
            resetPasswordDto.OldPassword,
            resetPasswordDto.NewPassword);

        return result.Succeeded;
    }

    public async Task<bool> ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto)
    {
        var user = await _userManager.FindByNameAsync(forgotPasswordDto.UserName);

        if (user == null)
            return false;

        var removePasswordResult = await _userManager.RemovePasswordAsync(user);

        if (!removePasswordResult.Succeeded)
            return false;

        var addPasswordResult = await _userManager.AddPasswordAsync(user, forgotPasswordDto.NewPassword);

        return addPasswordResult.Succeeded;
    }

    private async Task<User> AuthenticateUserAsync(LoginDto model)
    {
        var result = await _signInManager.PasswordSignInAsync(
            model.UserName,
            model.Password,
            isPersistent: false,
            lockoutOnFailure: false);

        if (!result.Succeeded)
            return null!;

        return (await _userManager.FindByNameAsync(model.UserName))!;
    }

    private async Task<Shift> CreateShiftAsync(User user, DateTime startDateTime)
    {
        var shift = await _shiftRepository.GetLastUserOpenedShiftAsync(user.Id);

        if (shift is not null)
            return shift;

        shift = new Shift
        {
            StartDateTime = startDateTime,
            LocationId = user.LocationId,
            CashierUserId = user.Id,
            Status = ShiftStatus.Opened
        };

        await _shiftRepository.CreateAsync(shift);

        var shiftAdded = await _unitOfWork.Complete();

        return shiftAdded ? shift : null!;
    }

    private async Task<string> GetToken(User user)
    {
        var claims = new List<TokenClaim>
        {
            new("userId", user.Id.ToString()),
            new("userName", user.UserName!),
            new("email", user.Email!),
            new("role", user.Role.Name!)
        };

        return await _tokensService.GenerateToken(claims);
    }
}
