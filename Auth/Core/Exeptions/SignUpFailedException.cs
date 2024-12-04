namespace Core.Exeptions;

public class SignUpFailedException : Exception
{
    public SignUpFailedException(string error) : base(error)
    {
    }
}