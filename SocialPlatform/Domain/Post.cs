using System;
using System.Collections.Generic;
using SocialPlatform.Domain;
using SocialPlatform.Interfaces;

namespace SocialPlatform.Domain
{
    public abstract class Post : Content, ILikeable, ICommentable, IShareable
    {
        // HashSet-ийн оронд EF Core дэмжих List<Guid> ашиглана
        public List<Guid> Likes { get; set; } = new();
        public List<string> Comments { get; set; } = new();
        public List<Guid> Shares { get; set; } = new();

        public int LikeCount => Likes.Count;
        public int HowManyComment => Comments.Count;
        public int ShareCount => Shares.Count;

        protected Post() : base() { }

        protected Post(Guid authorId, string contentText)
            : base(authorId, contentText)
        {
        }

        // List дээр давхардаж like/share дарагдахаас сэргийлэх шалгалт
        public virtual void AddLike(Guid userId)
        {
            if (!Likes.Contains(userId)) Likes.Add(userId);
        }

        public virtual void RemoveLike(Guid userId) => Likes.Remove(userId);
        public virtual void AddComment(Guid userId, string text) => Comments.Add(text);

        public virtual void AddShare(Guid userId)
        {
            if (!Shares.Contains(userId)) Shares.Add(userId);
        }

        public IReadOnlyList<string> GetComments() => Comments.AsReadOnly();
    }
}