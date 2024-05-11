using AgoraphobiaLibrary.Exceptions;

namespace AgoraphobiaLibrary
{
    
    public record Account
    {
        public Account(int id, string username, string password, bool isPasswordHashed = false)
        {
            Username = username;
            Password = new Password(password, isPasswordHashed);
            Id = id;
        }
        private const int MINIMUM_LENGTH = 6;
        private const int MAXIMUM_LENGTH = 32;
        public int Id { get; }
        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                if (value.Length < MINIMUM_LENGTH)
                    throw new TooShortUsernameException(MINIMUM_LENGTH);
                if (value.Length > MAXIMUM_LENGTH)
                    throw new TooLongUsernameException(MAXIMUM_LENGTH);
                _username = value;
            }
        }
        public Password Password { get; }
    }
}
