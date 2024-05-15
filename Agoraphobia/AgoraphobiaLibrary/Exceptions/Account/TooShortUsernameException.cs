namespace AgoraphobiaLibrary.Exceptions.Account;

public class TooShortUsernameException : Exception
{
    public TooShortUsernameException(int minimumLength)
        : base($"Username needs to be at least {minimumLength} characters long!")
    {
        
    }
}