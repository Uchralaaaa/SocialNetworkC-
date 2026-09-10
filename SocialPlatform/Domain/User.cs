using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace SocialPlatform.Domain
{
    public class User
    {
        public Guid UserId { get; set; } = Guid.NewGuid();
        public string UserName { get; set; }
        public DateTime DOB { get; }
        public byte Age => (byte)(DateTime.UtcNow.Year - DOB.Year);

        private string _password = string.Empty;
        public string PassWord 
        {
            get => _password;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(value), "Password cannot be empty!");
                }
                if (value.Length < 8)
                {
                    throw new ArgumentException("Password must be atleast 8 character!");
                }
                _password = value;
            }
        }

        public string ProfilePictureURL { get; set; } = "https://example.com/default-avatar.png";

        public User(string username, DateTime dob, string password, string? profilePictureURL)
        {
            UserName = username;
            DOB = dob;
            _password = password;

            if (!string.IsNullOrWhiteSpace(profilePictureURL))
            {
                ProfilePictureURL = profilePictureURL;
            }
        }
    }
}
