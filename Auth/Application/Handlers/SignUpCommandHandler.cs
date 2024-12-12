using Application.Commands;
using Core.DTO;
using Core.Exeptions;
using Core.Interfaces.Services;
using Core.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Handlers;

public class SignUpCommandHandler : IRequestHandler<SignUpCommand, AuthModel>
{
    private readonly UserManager<User> _userManager;
    private readonly IJwtService _jwtService;


    public SignUpCommandHandler(UserManager<User> userManager, IJwtService jwtService)
    {
        _userManager = userManager;
        _jwtService = jwtService;
    }

    public async Task<AuthModel> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        var user = new User
        {
            UserName = request.UserName,
            Email = request.Email
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
            throw new SignUpFailedException(
                string.Join(", ",
                    result.Errors.Select(x => x.Description)));

        return new AuthModel
        {
            AccessToken = _jwtService.GenerateJwt(user.Id, user.UserName)
        };
    }
}