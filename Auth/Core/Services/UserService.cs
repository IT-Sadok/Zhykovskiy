using Core.DTO;
using Core.Exeptions;
using Core.Interfaces.Services;
using Core.Models;
using Microsoft.AspNetCore.Identity;

namespace Core.Services;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly IJwtService _jwtService;

    public UserService(UserManager<User> userManager, IJwtService jwtService)
    {
        _userManager = userManager;
        _jwtService = jwtService;
    }

    public async Task<AuthModel> RegisterUserAsync(SignUpModel model)
    {
        var user = new User
        {
            UserName = model.Username,
            Email = model.Email
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
            throw new SignUpFailedException(
                string.Join(", ",
                    result.Errors.Select(x => x.Description)));

        return new AuthModel
        {
            AccessToken = _jwtService.GenerateJwt(user.Id, user.UserName)
        };
    }

    public async Task<AuthModel> LoginUserAsync(SignInModel model)
    {
        var user = await _userManager.FindByNameAsync(model.UserName);
        if (user == null) throw new SignInFailedException("User not found");
        var isPasswordValid = await _userManager.CheckPasswordAsync(user, model.Password);

        if (!isPasswordValid) throw new SignInFailedException("Invalid password");

        return new AuthModel
        {
            AccessToken = _jwtService.GenerateJwt(user.Id, user.UserName!)
        };
    }
}