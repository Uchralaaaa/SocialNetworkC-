using System;

namespace SocialPlatform.Domain
{
    public class User
    {
        public Guid UserId { get; set; } = Guid.NewGuid();
        public string UserName { get; set; }

        // EF Core баазаас утга олгох боломжтой болгох үүднээс private set нэмэв
        public DateTime DOB { get; private set; }

        // Насыг сар, өдөр харгалзан зөв тооцоолох
        public byte Age
        {
            get
            {
                var today = DateTime.UtcNow;
                var age = today.Year - DOB.Year;
                if (DOB.Date > today.AddYears(-age)) age--;
                return (byte)age;
            }
        }

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
                    throw new ArgumentException("Password must be at least 8 characters!");
                }
                _password = value;
            }
        }

        public string ProfilePictureURL { get; set; } = "https://example.com/default-avatar.png";

        // EF Core-д зориулсан параметргүй байгуулагч
        protected User()
        {
            UserName = string.Empty;
        }

        public User(string username, DateTime dob, string password, string? profilePictureURL = null)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("Username cannot be empty!", nameof(username));
            }

            UserName = username;
            DOB = dob;

            // Property-д оноож шалгуурыг ажиллуулна
            PassWord = password;

            if (!string.IsNullOrWhiteSpace(profilePictureURL))
            {
                ProfilePictureURL = profilePictureURL;
            }
        }
    }
}