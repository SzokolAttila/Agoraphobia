using AgoraphobiaLibrary.Exceptions;

namespace AgoraphobiaLibrary
{
    
    public record Account
    {
        public Account(int id, string username, string password, bool isPasswordHashed = false)
        {
            if (username.Length < MINIMUM_LENGTH)
                throw new TooShortUsernameException(MINIMUM_LENGTH);
            if (username.Length > MAXIMUM_LENGTH)
                throw new TooLongUsernameException(MAXIMUM_LENGTH);
            Username = username;
            Password = new Password(password, isPasswordHashed);
            Id = id;
        }
        private const int MINIMUM_LENGTH = 6;
        private const int MAXIMUM_LENGTH = 32;
        public int Id { get; }
        public string Username { get; set; }
        public string HashedPassword => Password.HashedPassword;
        private Password Password { get; set; }
    }
}
