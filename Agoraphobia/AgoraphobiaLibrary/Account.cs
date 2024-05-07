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
        public int Id { get; init; }
        public string Username { get; set; }
        public string HashedPassword => Password.HashedPassword;
        private Password Password { get; set; }
    }
}
