using Core.DTO;

namespace Core.Interfaces.Services;

public interface IUserService
{
    Task<AuthModel> RegisterUserAsync(SignUpModel model);

    Task<AuthModel> LoginUserAsync(SignInModel model);
}