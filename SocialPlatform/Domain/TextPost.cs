using System;
using System.Collections.Generic;
using System.Text;
using SocialPlatform.Domain;

namespace SocialPlatform.Domain
{
    public class TextPost : Post
    {
        public const int MaxLen = 280;

        // EF Core-д зориулсан байгуулагч
        protected TextPost() : base() { }

        public TextPost(Guid authorId, string contentText)
            : base(authorId, contentText)
        {
            if (contentText != null && contentText.Length > MaxLen)
            {
                throw new ArgumentException($"The post cannot exceed {MaxLen} characters!");
            }
        }
    }
}