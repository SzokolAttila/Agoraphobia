using System.Data;
using System.Security.Cryptography;
using AgoraphobiaLibrary;
using AgoraphobiaLibrary.Exceptions;

namespace AgoraphobiaTests
{
    [TestClass]
    public class PasswordTests
    {
        [TestMethod]
        public void EmptyPasswordThrowsException()
        {
            Assert.ThrowsException<EmptyFieldException>(() => new Password(""));
        }

        [TestMethod]
        public void HashedPasswordCanBeAccessed()
        {
            var password = new Password("Hululu!0");
            Console.WriteLine(password.HashedPassword);
            Assert.AreEqual(
                System.Text.Encoding.UTF8.GetString(SHA512.HashData(System.Text.Encoding.UTF8.GetBytes("Hululu!0"))),
                password.HashedPassword);
        }

        [TestMethod]
        public void AlreadyHashedPasswordIsNotHashedAgain()
        {
            var password = new Password("Hululu!0", true);
            Assert.AreEqual("Hululu!0", password.HashedPassword);
        }

        [TestMethod]
        public void EmptyStringSecurityIs0()
        {
            Assert.AreEqual(0, Password.CheckSecurityLevel(""));
        }
        
        [TestMethod]
        [DataRow("a")]
        [DataRow("aaoeu")]
        public void LowercaseCharacterSecurityIs1(string password)
        {
            Assert.AreEqual(1, Password.CheckSecurityLevel(password));
        }

        [TestMethod]
        [DataRow("UTOENA")]
        [DataRow("U")]
        public void UppercaseCharacterSecurityIs2(string password)
        {
            Assert.AreEqual(2, Password.CheckSecurityLevel(password));
        }

        [TestMethod]
        [DataRow("13")]
        [DataRow("2113")]
        public void DigitSecurityLevelIs4(string password)
        {
            Assert.AreEqual(4, Password.CheckSecurityLevel(password));
        }

        [TestMethod]
        [DataRow("(}{=}")]
        [DataRow("(/{}")]
        [DataRow(",")]
        [DataRow(".")]
        [DataRow("\\")]
        public void SpecialCharacterSecurityLevelIs8(string password)
        {
            Assert.AreEqual(8, Password.CheckSecurityLevel(password));
        }

        [TestMethod]
        [DataRow(3, "Aa")]
        [DataRow(7, "Aa3")]
        [DataRow(9, "a/")]
        [DataRow(10, "A/")]
        [DataRow(11, "Aa/")]
        [DataRow(15, "Aa/3")]
        public void SecurityValuesAddUp(int securityValue, string password)
        {
            Assert.AreEqual(securityValue, Password.CheckSecurityLevel(password));
        }

        [TestMethod]
        [DataRow(17, "aaaaaaaaaaa")]
        [DataRow(19, "aaaaAaaaaaaaaa")]
        [DataRow(31, "aaaaAaa3(aaa")]
        public void EightCharOrLongerSecurityLevelIs16(int securityValue, string password)
        {
            Assert.AreEqual(securityValue, Password.CheckSecurityLevel(password));
        }

        [TestMethod]
        [DataRow("Aeoduh")]
        [DataRow("Ae3}uh")]
        [DataRow("Ae{oaeuuh")]
        public void NotSecurePasswordThrowsException(string password)
        {
            Assert.ThrowsException<NotSecurePasswordException>(() => new Password(password));
        }
    }
}