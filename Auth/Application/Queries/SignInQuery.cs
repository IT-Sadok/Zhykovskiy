using Core.DTO;
using MediatR;

namespace Application.Queries;

public record SignInQuery(string UserName, string Password) : IRequest<AuthModel>;