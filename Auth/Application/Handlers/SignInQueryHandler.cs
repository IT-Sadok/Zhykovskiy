using Application.Queries;
using Core.DTO;
using Core.Exeptions;
using Core.Interfaces.Services;
using Core.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Handlers;

public class SignInQueryHandler : IRequestHandler<SignInQuery, AuthModel>
{
    private readonly UserManager<User> _userManager;
    private readonly IJwtService _jwtService;


    public SignInQueryHandler(UserManager<User> userManager, IJwtService jwtService)
    {
        _userManager = userManager;
        _jwtService = jwtService;
    }

    public async Task<AuthModel> Handle(SignInQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.UserName);
        if (user == null) throw new SignInFailedException("User not found");
        var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);

        if (!isPasswordValid) throw new SignInFailedException("Invalid password");

        return new AuthModel
        {
            AccessToken = _jwtService.GenerateJwt(user.Id, user.UserName!)
        };
    }
}