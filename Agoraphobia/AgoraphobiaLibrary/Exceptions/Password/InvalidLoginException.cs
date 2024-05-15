namespace AgoraphobiaLibrary.Exceptions.Password
{
    public class InvalidLoginException : Exception
    {
        public InvalidLoginException()
            : base("Invalid username or password!")
        {

        }
    }
}
