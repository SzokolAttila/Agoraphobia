using AgoraphobiaLibrary;
using AgoraphobiaLibrary.Exceptions;
using AgoraphobiaLibrary.Exceptions.Account;

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
    }
}
