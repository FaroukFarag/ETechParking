using AutoMapper;
using ETechParking.Application.Dtos.Locations.Users;
using ETechParking.Application.Interfaces.Locations.Users;
using ETechParking.Application.Services.Abstraction;
using ETechParking.Common.Tokens.Interfaces;
using ETechParking.Domain.Interfaces.Repositories.Abstraction;
using ETechParking.Domain.Interfaces.Repositories.Locations.Roles;
using ETechParking.Domain.Interfaces.UnitOfWork;
using ETechParking.Domain.Models.Locations.Users;
using Microsoft.AspNetCore.Identity;

namespace ETechParking.Application.Services.Locations.Users;

public class UserService(
    IBaseRepository<User, int> repository,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    SignInManager<User> signInManager,
    UserManager<User> userManager,
    IRoleRepository roleRepository,
    ITokensService tokensService) :
    BaseService<User, UserDto, int>(repository, unitOfWork, mapper), IUserService
{
    private readonly IMapper _mapper = mapper;
    private readonly SignInManager<User> _signInManager = signInManager;
    private readonly UserManager<User> _userManager = userManager;
    private readonly IRoleRepository _roleRepository = roleRepository;
    private readonly ITokensService _tokensService = tokensService;

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

    public async override Task<UserDto> Update(UserDto newUserDto)
    {
        var user = _mapper.Map<User>(newUserDto);
        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
            return default!;

        return newUserDto;
    }

    public async Task<string> LoginAsync(LoginDto model)
    {
        var result = await _signInManager.PasswordSignInAsync(
            model.UserName,
            model.Password,
            false,
            lockoutOnFailure: false);

        if (!result.Succeeded)
            return default!;

        var user = await _userManager.FindByNameAsync(model.UserName);

        return await GetToken(user);
    }

    private async Task<string> GetToken(User? user)
    {
        var claims = new List<TokenClaim>
        {
            new("UserName", user?.UserName!),
            new("Email", user?.Email!),
            new("LocationId", user?.LocationId.ToString()!)
        };

        return await _tokensService.GenerateToken(claims);
    }
}
