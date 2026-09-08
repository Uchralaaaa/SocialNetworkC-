using System;
using System.Collections.Generic;
using System.Text;

namespace SocialPlatform.Domain
{
    public class Comment :  Content
    {
        public DateTime CommentWrittenAt { get; set; } = DateTime.Now;
        protected readonly HashSet<Guid> Likes = new();
        public int LikeCount => Likes.Count;
        public Comment(Guid authorId, string contentText)
            : base(authorId, contentText)
        {
        }
    }
}
