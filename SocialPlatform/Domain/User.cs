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
        public string PassWord 
        {
            protected get;
            set 
            { 
                if (value == null) {throw new ArgumentNullException("value"); }
                if (value.Length < 8) { throw new ArgumentException(); }
            }
        }

        public string ProfilePictureURL { get; set; } = "https://example.com/default-avatar.png";
    }
}
