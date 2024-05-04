namespace AgoraphobiaLibrary
{
    public class Accounts
    {
        private List<Account> AccountsList { get; set; }
        public Accounts(IEnumerable<Account> accounts)
        {
            AccountsList = accounts.ToList();
        }
        public Account? Login(string username, string password, bool isPasswordHashed = false)
        {
            var passwd = new Password(password, isPasswordHashed);
            return AccountsList.Find(x => 
                x.Password.HashedPassword == passwd.HashedPassword
                && x.Username == username);
        }
    }
}
