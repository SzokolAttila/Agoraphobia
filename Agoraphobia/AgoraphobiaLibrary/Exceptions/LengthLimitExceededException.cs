namespace AgoraphobiaLibrary.Exceptions;

public class LengthLimitExceededException : Exception
{
    public LengthLimitExceededException()
        : base($"The maximum length for an input string is {LENGTH_LIMIT  }!")
    {
        
    }
    public const int LENGTH_LIMIT = 20;
}