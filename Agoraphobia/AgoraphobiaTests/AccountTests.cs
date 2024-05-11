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
            Assert.AreEqual("delulu", account.Username);
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
        public void NonUniqueUsernameThrowsException()
        {
            var accounts = new Accounts(new List<Account>()
            {
                new Account(1, "delulu", "Hululu!0"),
                new Account(2, "jackie", "Hululu!0")
            });
            Assert.ThrowsException<NonUniqueUsernameException>(() => accounts.CreateAccount(3, "jackie", "Delulu!0"));
        }
        [TestMethod]
        public void UsernameCanBeUpdated()
        {
            var accounts = new Accounts(new List<Account>()
            {
                new Account(1, "delulu", "Hululu!0"),
                new Account(2, "jackie", "Hululu!0")
            });
            accounts.UpdateAccount(2, "brownie", "Hululu!0", "Hululu!0", "Hululu!0");
            var updated = accounts.GetAccount(2)!;
            Assert.AreEqual("brownie", updated.Username);
        }

        [TestMethod]
        public void SameLengthLimitRulesApplyToUpdatedUsernames()
        {
            var accounts = new Accounts(new List<Account>()
            {
                new Account(1, "delulu", "Hululu!0"),
                new Account(2, "jackie", "Hululu!0")
            });
            Assert.ThrowsException<TooShortUsernameException>(() => accounts.UpdateAccount(2, "aoe", "Hululu!0", "Hululu!0", "Hululu!0"));
            Assert.ThrowsException<TooLongUsernameException>(() => accounts.UpdateAccount(2, "auaeouhteoanuheroatuhneoahunteoheuntaooe", "Hululu!0", "Hululu!0", "Hululu!0"));
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

        [TestMethod]
        public void PasswordOfAccountCanBeChanged()
        {
            var accounts = new Accounts(new List<Account>()
            {
                new Account(1, "delulu", "Hululu!0"),
                new Account(2, "jackie", "Hululu!0")
            });
            accounts.UpdateAccount(1,"delulu", "Hululu!0", "Delulu!0", "Delulu!0");
            var account = accounts.Login("delulu", "Delulu!0");
            Assert.AreEqual(1, account.Id);
        }
    }
}
