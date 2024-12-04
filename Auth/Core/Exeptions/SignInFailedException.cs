namespace Core.Exeptions;

public class SignInFailedException : Exception
{
    public SignInFailedException(string error) : base(error)
    {
    }
}