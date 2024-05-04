namespace AgoraphobiaLibrary.Exceptions;

public class TooShortUsernameException : Exception
{
    public TooShortUsernameException()
        : base($"Username needs to be at least {MINIMUM_LENGTH} characters long!")
    {
        
    }
    public const int MINIMUM_LENGTH = 6;
}