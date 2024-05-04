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
                new Account("hululu", "Hululu!0")
            });
            Assert.AreEqual(null, accounts.Login("delulu", "Hululu!0"));
        }

        [TestMethod]
        public void LoginFunctionReturnsAccountLoggedInto()
        {
            var accounts = new Accounts(new List<Account>()
            {
                new Account("delulu", "Hululu!0")
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
                new Account("delulu", "Hululu!0", true)
            });
            var account = accounts.Login("delulu", "Hululu!0", true);
            Assert.AreEqual("delulu", account!.Username);
            Assert.AreEqual("Hululu!0", account.Password.HashedPassword);
        }
    }
}
