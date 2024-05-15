using System.Security.Cryptography;
using AgoraphobiaLibrary;
using AgoraphobiaLibrary.Exceptions;
using AgoraphobiaLibrary.Exceptions.Password;

namespace AgoraphobiaTests
{
    [TestClass]
    public class PasswordTests
    {
        [TestMethod]
        public void HashedPasswordCanBeAccessed()
        {
            var password = new Password("Delulu!0");
            Console.WriteLine(password.Passwd);
            Assert.AreEqual(
                System.Text.Encoding.UTF8.GetString(SHA512.HashData(System.Text.Encoding.UTF8.GetBytes("Delulu!0"))),
                password.Passwd);
        }

        [TestMethod]
        public void AlreadyHashedPasswordIsNotHashedAgain()
        {
            var password = new Password("Hululu!0", true);
            Assert.AreEqual("Hululu!0", password.Passwd);
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
        [DataRow("eohtanueoau")]
        [DataRow("eohtanA}oau")]
        [DataRow("5A}oau")]
        public void NotSecurePasswordThrowsException(string password)
        {
            Assert.ThrowsException<NotSecurePasswordException>(() => new Password(password));
        }

        [TestMethod]
        public void PasswordCanBeChanged()
        {
            var password = new Password("Hululu!0");
            var temp = new Password("Hululu!0");
            Assert.AreEqual(password.Passwd, temp.Passwd);
            password.ChangePassword("Hululu!0",  "Delulu!0");
            Assert.AreNotEqual(password.Passwd, temp.Passwd);
        }

        [TestMethod]
        public void ChangingPasswordThrowsExceptionIfOldPasswordIsIncorrect()
        {
            var password = new Password("Hululu!0");
            Assert.ThrowsException<IncorrectPasswordException>(() =>
                password.ChangePassword("Delulu!0", "Delulu!0"));
        }

        [TestMethod]
        [DataRow("euohtaueueohnta")]
        [DataRow("Ooue3{")]
        [DataRow("Ooue3oeueo")]
        [DataRow("Ooue}oeueo")]
        public void PasswordSecurityRequirementsApplyToChangedPassword(string passwd)
        {
            var password = new Password("Delulu!0");
            Assert.ThrowsException<NotSecurePasswordException>(() => 
                password.ChangePassword("Delulu!0", passwd));
        }
    }
}