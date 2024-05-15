using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using AgoraphobiaLibrary.Exceptions;

namespace AgoraphobiaLibrary
{
    public record Account
    {
        public Account(string username, string password, bool isPasswordHashed)
        {
            Username = username;
            Password = new Password(password, isPasswordHashed);
        }
        [JsonConstructor]
        public Account(int id, string username, string passwd, bool isPasswordHashed = false)
        {
            Username = username;
            Password = new Password(passwd, isPasswordHashed);
            Id = id;
        }
        [Key]
        public int Id { get; private set; }
        private string _username;
        [Required]
        [MaxLength(32)]
        public string Username
        {
            get => _username;
            set
            {
                if (value.Length < MINIMUM_LENGTH)
                    throw new TooShortUsernameException(MINIMUM_LENGTH);
                if (value.Length > MAXIMUM_LENGTH)
                    throw new TooLongUsernameException(MAXIMUM_LENGTH);
                _username = value;
            }
        }
        [JsonInclude]
        public string Passwd
        {
            get => Password.Passwd;
            private set
            {
                
            }
        }
        [JsonInclude] 
        public bool IsPasswordHashed
        {
            get => Password.IsHashed;
            private set
            {
                
            }
        }
        [JsonIgnore]
        public Password Password { get; private set; }
        private const int MINIMUM_LENGTH = 6;
        private const int MAXIMUM_LENGTH = 32;
    }
}
