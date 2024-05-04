namespace AgoraphobiaLibrary.Exceptions
{
    public class EmptyFieldException : Exception
    {
        public EmptyFieldException()
            : base("A required field has been left empty!")
        {

        }
    }
}
