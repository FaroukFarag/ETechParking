using AutoMapper;
using ETechParking.Application.Dtos.Users;
using ETechParking.Application.Interfaces.Users;
using ETechParking.Common.Tokens.Interfaces;
using ETechParking.Domain.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace ETechParking.Application.Services.Users;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ITokensService _tokensService;
    private readonly IMapper _mapper;

    public UserService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        ITokensService tokensService,
        IMapper mapper)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokensService = tokensService;
        _mapper = mapper;
    }

    public async Task<string> RegisterAsync(RegisterDto model)
    {
        var user = _mapper.Map<ApplicationUser>(model);
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

    private async Task<string> GetToken(ApplicationUser? user)
    {
        var claims = new List<TokenClaim>
        {
            new TokenClaim("UserName", user?.UserName!),
            new TokenClaim("Email", user?.Email!)
        };

        return await _tokensService.GenerateToken(claims);
    }
}
