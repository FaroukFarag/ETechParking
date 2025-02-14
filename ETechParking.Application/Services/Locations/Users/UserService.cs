using AutoMapper;
using ETechParking.Application.Dtos.Locations.Users;
using ETechParking.Application.Dtos.Shared;
using ETechParking.Application.Interfaces.Locations.Users;
using ETechParking.Application.Services.Abstraction;
using ETechParking.Common.Tokens.Interfaces;
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

    public async Task<LoggedInDto> LoginAsync(LoginDto model)
    {
        var result = await _signInManager.PasswordSignInAsync(
            model.UserName,
            model.Password,
            false,
            lockoutOnFailure: false);

        if (!result.Succeeded)
            return default!;

        var user = (await _userManager.FindByNameAsync(model.UserName))!;
        var shift = await _shiftRepository.CreateAsync(new Shift { LocationId = user.LocationId, UserId = user.Id });

        var shiftAdded = await _unitOfWork.Complete();

        if (!shiftAdded)
            return default!;

        var loggedInDto = new LoggedInDto
        {
            LocationId = user.LocationId,
            ShiftId = shift.Id,
            Token = await GetToken(user)
        };

        return loggedInDto;
    }

    private async Task<string> GetToken(User? user)
    {
        var claims = new List<TokenClaim>
        {
            new("UserName", user?.UserName!),
            new("Email", user?.Email!)
        };

        return await _tokensService.GenerateToken(claims);
    }
}
