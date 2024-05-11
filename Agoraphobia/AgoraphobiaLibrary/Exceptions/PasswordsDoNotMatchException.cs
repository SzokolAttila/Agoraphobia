namespace AgoraphobiaLibrary.Exceptions;

public class PasswordsDoNotMatchException : Exception
{
    public PasswordsDoNotMatchException()
        : base("The passwords you have provided don't match!")
    {
        
    }
}