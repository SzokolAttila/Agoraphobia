using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using AgoraphobiaLibrary.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace AgoraphobiaLibrary
{
    [NotMapped]
    [Keyless]
    public class Password
    {
        private string _password;
        public bool IsHashed { get; private set; }
        private const int MAX_SECURITY_LEVEL = 31;
        public Password(string passwd, bool isHashed = false)
        {
            if (CheckSecurityLevel(passwd) != MAX_SECURITY_LEVEL)
                throw new NotSecurePasswordException();
            IsHashed = isHashed;
            _password = passwd;
        }
        
        public string Passwd
        {
            get => IsHashed ? _password : 
                System.Text.Encoding.UTF8.GetString(SHA512.HashData(System.Text.Encoding.UTF8.GetBytes(_password)));
            private set
            { 
                
            }
        }
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

        public void ChangePassword(string oldPassword, string newPassword, string newPasswordAgain)
        {
            if (System.Text.Encoding.UTF8.GetString(SHA512.HashData(System.Text.Encoding.UTF8.GetBytes(oldPassword))) != Passwd)
                throw new IncorrectPasswordException();
            if (newPassword != newPasswordAgain)
                throw new PasswordsDoNotMatchException();
            if (CheckSecurityLevel(newPassword) != MAX_SECURITY_LEVEL)
                throw new NotSecurePasswordException();
            _password = newPassword;
            IsHashed = false;
        }
    }
}