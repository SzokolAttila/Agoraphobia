namespace AgoraphobiaLibrary
{
    public class Account
    {
        public Account(string username, string password, bool isPasswordHashed = false)
        {
            Username = username;
            Password = new Password(password, isPasswordHashed);
        }

        public string Username { get; set; }
        public Password Password { get; set; }
    }
}
