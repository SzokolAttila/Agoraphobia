namespace AgoraphobiaLibrary.Exceptions.Account;

public class TooLongUsernameException : Exception
{
    public TooLongUsernameException(int lengthLimit)
        : base($"The maximum character length for a username is {lengthLimit}!")
    {
        
    }
}