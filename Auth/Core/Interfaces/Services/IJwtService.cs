namespace Core.Interfaces.Services;

public interface IJwtService
{
    public string GenerateJwt(int id, string userName);
}