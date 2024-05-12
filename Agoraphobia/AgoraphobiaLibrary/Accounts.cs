using AgoraphobiaLibrary.Exceptions;

namespace AgoraphobiaLibrary
{
    public class Accounts
    {
        private List<Account> AccountsList { get; set; }
        public Accounts(IEnumerable<Account> accounts)
        {
            AccountsList = accounts.ToList();
        }
        public Account Login(string username, string password, bool isPasswordHashed = false)
        {
            var passwd = new Password(password, isPasswordHashed);
            var account = AccountsList.Find(x => 
                x.Username == username
                && x.Password.HashedPassword == passwd.HashedPassword);
            if (account is null)
                throw new InvalidLoginException();
            return account;
        }
        public IEnumerable<Account> GetAccounts() => AccountsList.Select(x => x);
        public Account? GetAccount(int id) => AccountsList.SingleOrDefault(x => x.Id == id);

        public Account CreateAccount(int id, string username, string password)
        {
            if (GetAccount(id) is not null)
                throw new NonUniqueIdException();
            if (AccountsList.Exists(x => x.Username == username))
                throw new NonUniqueUsernameException();
            var account = new Account(id, username, password);
            AccountsList.Add(account);
            return account;
        }
        public Account UpdateAccount(int id, string username, string oldPassword, string newPassword, string newPasswordAgain)
        {
            AccountsList = AccountsList.Select(x =>
            {
                if (x.Id == id)
                {
                    x.Password.ChangePassword(oldPassword, newPassword, newPasswordAgain);
                    x.Username = username;
                }
                return x;
            }).ToList();
            return GetAccount(id)!;
        }

        public void DeleteAccount(int id)
        {
            AccountsList = AccountsList.FindAll(x => x.Id != id);
        }
    }
}
