using System.Xml.XPath;

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
                x.HashedPassword == passwd.HashedPassword
                && x.Username == username);
        }
        public IEnumerable<Account> GetAccounts() => AccountsList.Select(x => x);
        public Account? GetAccount(int id) => AccountsList.SingleOrDefault(x => x.Id == id);

        public Account CreateAccount(Account account)
        {
            AccountsList.Add(account);
            return account;
        }

        public Account UpdateAccount(Account account)
        {
            AccountsList = AccountsList.Select(x =>
            {
                if (x.Id == account.Id)
                    x.Username = account.Username;
                return x;
            }).ToList();
            return account;
        }

        public void DeleteAccount(int id)
        {
            AccountsList = AccountsList.FindAll(x => x.Id != id);
        }
    }
}
