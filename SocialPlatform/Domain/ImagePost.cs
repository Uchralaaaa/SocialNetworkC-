using System;
using System.Collections.Generic;
using System.Text;
using SocialPlatform.Interfaces;

namespace SocialPlatform.Domain
{
    public class ImagePost : Post
    {
        public string ImageURL { get; set; } = string.Empty;

        // EF Core-д зориулсан байгуулагч
        protected ImagePost() : base() { }

        public ImagePost(Guid authorId, string contentText, string imageURL)
            : base(authorId, contentText)
        {
            if (string.IsNullOrWhiteSpace(imageURL))
            {
                throw new ArgumentException("Cannot be empty!", nameof(imageURL));
            }

            ImageURL = imageURL;
        }
    }
}