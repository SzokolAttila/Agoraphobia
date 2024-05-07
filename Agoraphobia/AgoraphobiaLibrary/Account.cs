namespace AgoraphobiaLibrary
{
    public class Account
    {
        public Account(int id, string username, string password, bool isPasswordHashed = false)
        {
            Username = username;
            Password = new Password(password, isPasswordHashed);
            Id = id;
        }
        public int Id { get; init; }
        public string Username { get; private set; }
        public Password Password { get; private set; }
    }
}
