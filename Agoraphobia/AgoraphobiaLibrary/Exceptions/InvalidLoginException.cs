namespace AgoraphobiaLibrary.Exceptions
{
    public class InvalidLoginException : Exception
    {
        public InvalidLoginException()
            : base("Invalid username or password!")
        {

        }
    }
}
