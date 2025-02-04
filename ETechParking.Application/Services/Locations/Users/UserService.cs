using AutoMapper;
using ETechParking.Application.Dtos.Locations.Users;
using ETechParking.Application.Interfaces.Users;
using ETechParking.Common.Tokens.Interfaces;
using ETechParking.Domain.Models.Locations.Users;
using Microsoft.AspNetCore.Identity;

namespace ETechParking.Application.Services.Locations.Users;

public class UserService(
    UserManager<User> userManager,
    SignInManager<User> signInManager,
    ITokensService tokensService,
    IMapper mapper) : IUserService
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly SignInManager<User> _signInManager = signInManager;
    private readonly ITokensService _tokensService = tokensService;
    private readonly IMapper _mapper = mapper;

    public async Task<string> RegisterAsync(RegisterDto model)
    {
        var user = _mapper.Map<User>(model);
        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
            return default!;

        return await GetToken(user);
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
