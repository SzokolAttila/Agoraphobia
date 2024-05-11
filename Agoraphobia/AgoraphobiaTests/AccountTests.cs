using System.Security.Cryptography;
using System.Text;
using AgoraphobiaLibrary;
using AgoraphobiaLibrary.Exceptions;

namespace AgoraphobiaTests
{
    [TestClass]
    public class AccountTests
    {
        [TestMethod]
        public void TooShortUsernameThrowsException()
        {
            Assert.ThrowsException<TooShortUsernameException>(() => new Account(3, "eoua", "eohuaeotna"));
        }

        [TestMethod]
        public void TooLongUsernameThrowsException()
        {
            Assert.ThrowsException<TooLongUsernameException>(() =>
                new Account(7, "eohaunheoanuheoanthueoanthuehoantuheoantueohnt", "uoaen"));
        }
        [TestMethod]
        public void LoginFunctionThrowsExceptionWhenInvalid()
        {
            var accounts = new Accounts(new List<Account>()
            {
                new Account(1,"hululu", "Hululu!0")
            });
            Assert.ThrowsException<InvalidLoginException>(() => accounts.Login( "delulu", "Hululu!0"));
        }

        [TestMethod]
        public void LoginFunctionReturnsAccountLoggedInto()
        {
            var accounts = new Accounts(new List<Account>()
            {
                new Account(1, "delulu", "Hululu!0")
            });
            var account = accounts.Login("delulu", "Hululu!0");
            Assert.AreEqual("delulu", account.Username);
            Assert.AreEqual(Encoding.UTF8.GetString(SHA512.HashData(Encoding.UTF8.GetBytes("Hululu!0"))), account.HashedPassword);
        }

        [TestMethod]
        public void AccountCanBeCreatedWithHashedPassword()
        {
            var accounts = new Accounts(new List<Account>()
            {
                new Account(1, "delulu", "Hululu!0", true)
            });
            var account = accounts.Login("delulu", "Hululu!0", true);
            Assert.AreEqual("delulu", account.Username);
            Assert.AreEqual("Hululu!0", account.HashedPassword);
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
            accounts.CreateAccount(3, "newAccount", "Delulu!0"); 
            Assert.AreEqual(3, accounts.GetAccounts().Count());
        }

        [TestMethod]
        public void NonUniqueIdThrowsException()
        {
            var accounts = new Accounts(new List<Account>()
            {
                new Account(1, "delulu", "Hululu!0"),
                new Account(2, "jackie", "Hululu!0")
            });
            Assert.ThrowsException<NonUniqueIdException>(() => accounts.CreateAccount(2, "newAccount", "Delulu!0"));
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
            var updated = accounts.GetAccount(2)!;
            Assert.AreEqual("brownie", updated.Username);
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
