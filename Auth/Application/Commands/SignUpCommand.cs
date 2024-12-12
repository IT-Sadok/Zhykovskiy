using Core.DTO;
using MediatR;

namespace Application.Commands;

public record SignUpCommand(string UserName, string Email, string Password) : IRequest<AuthModel>;