using System.Security.Cryptography;
using System.Text.RegularExpressions;
using AgoraphobiaLibrary.Exceptions;

namespace AgoraphobiaLibrary
{
    public class Password
    {
        private readonly string _password;
        private readonly bool _isHashed;
        public Password(string password, bool isHashed = false)
        {
            if (password == "")
                throw new EmptyFieldException();
            if (CheckSecurityLevel(password) != 31)
                throw new NotSecurePasswordException();
            _password = password;
            _isHashed = isHashed;
        }
        public string HashedPassword => _isHashed ? _password : 
            System.Text.Encoding.UTF8.GetString(SHA512.HashData(System.Text.Encoding.UTF8.GetBytes(_password)));

        public static int CheckSecurityLevel(string password)
        {
            var securityLevel = 0;
            if (password == "")
                return securityLevel;
            if (Regex.IsMatch(password, "[a-z]"))
                securityLevel += 1;
            if (Regex.IsMatch(password, "[A-Z]"))
                securityLevel += 2;
            if (Regex.IsMatch(password, "[0-9]"))
                securityLevel += 4;
            if (Regex.IsMatch(password, @"[!-/:-@\[-`\{-~]"))
                securityLevel += 8;
            if (password.Length >= 8)
                securityLevel += 16;
            return securityLevel;
        }
    }
}