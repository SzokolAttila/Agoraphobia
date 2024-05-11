namespace AgoraphobiaLibrary.Exceptions;

public class IncorrectPasswordException : Exception
{
    public IncorrectPasswordException()
        : base("The password you have entered is incorrect!")
    {
        
    }
}