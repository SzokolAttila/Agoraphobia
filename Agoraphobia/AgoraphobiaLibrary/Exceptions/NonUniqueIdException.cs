namespace AgoraphobiaLibrary.Exceptions;

public class NonUniqueIdException : Exception
{
    public NonUniqueIdException()
        : base("The id assigned to the account is already in use!")
    {
        
    }
}