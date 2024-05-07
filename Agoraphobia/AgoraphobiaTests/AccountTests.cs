using System.Security.Cryptography;
using System.Text;
using AgoraphobiaLibrary;

namespace AgoraphobiaTests
{
    [TestClass]
    public class AccountTests
    {
        [TestMethod]
        public void LoginFunctionReturnsNullWhenInvalid()
        {
            var accounts = new Accounts(new List<Account>()
            {
                new Account(1,"hululu", "Hululu!0")
            });
            Assert.AreEqual(null, accounts.Login( "delulu", "Hululu!0"));
        }

        [TestMethod]
        public void LoginFunctionReturnsAccountLoggedInto()
        {
            var accounts = new Accounts(new List<Account>()
            {
                new Account(1, "delulu", "Hululu!0")
            });
            var account = accounts.Login("delulu", "Hululu!0");
            Assert.AreEqual("delulu", account!.Username);
            Assert.AreEqual(Encoding.UTF8.GetString(SHA512.HashData(Encoding.UTF8.GetBytes("Hululu!0"))), account.Password.HashedPassword);
        }

        [TestMethod]
        public void AccountCanBeCreatedWithHashedPassword()
        {
            var accounts = new Accounts(new List<Account>()
            {
                new Account(1, "delulu", "Hululu!0", true)
            });
            var account = accounts.Login("delulu", "Hululu!0", true);
            Assert.AreEqual("delulu", account!.Username);
            Assert.AreEqual("Hululu!0", account.Password.HashedPassword);
        }

        [TestMethod]
        public void AccountsCanBeAccessedThroughFunction()
        {
            var accounts = new Accounts(new List<Account>()
            {
                new Account(1, "delulu", "Hululu!0"),
                new Account(2, "jackie", "Hululu!0")
            });
            Assert.AreEqual(2, accounts.GetAccounts().Count());
        }
        [TestMethod]
        public void SpecificAccountCanBeAccessedWithId()
        {
            var accounts = new Accounts(new List<Account>()
            {
                new Account(1, "delulu", "Hululu!0"),
                new Account(2, "jackie", "Hululu!0")
            });
            var account = accounts.GetAccount(2)!; 
            Assert.AreEqual(2, account.Id);
            Assert.AreEqual("jackie", account.Username);
        }    
        
        [TestMethod]
        public void AccountCreatedCanBeAddedToAccounts()
        {
            var accounts = new Accounts(new List<Account>()
            {
                new Account(1, "delulu", "Hululu!0"),
                new Account(2, "jackie", "Hululu!0")
            });
            Assert.AreEqual(2, accounts.GetAccounts().Count());
            accounts.CreateAccount(new Account(3, "newAccount", "Delulu!0")); 
            Assert.AreEqual(3, accounts.GetAccounts().Count());
        }       
        [TestMethod]
        public void AccountsCanBeUpdated()
        {
            var accounts = new Accounts(new List<Account>()
            {
                new Account(1, "delulu", "Hululu!0"),
                new Account(2, "jackie", "Hululu!0")
            });
            var account = new Account(2, "brownie", "Hululu!0");
            accounts.UpdateAccount(account);
            var updated = accounts.GetAccount(2);
            Assert.AreEqual("brownie", account.Username);
        }
        [TestMethod]
        public void AccountsCanBeDeleted()
        {
            var accounts = new Accounts(new List<Account>()
            {
                new Account(1, "delulu", "Hululu!0"),
                new Account(2, "jackie", "Hululu!0")
            });
            Assert.AreEqual(2, accounts.GetAccounts().Count());
            accounts.DeleteAccount(1);
            Assert.AreEqual(1, accounts.GetAccounts().Count());
        }
    }
}
