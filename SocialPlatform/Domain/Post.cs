using System;
using System.Collections.Generic;
using System.Text;
using SocialPlatform.Domain;
using SocialPlatform.Interfaces;

namespace SocialPlatform.Domain;

public abstract class Post : Content, ILikeable, ICommentable, IShareable
{
    protected readonly HashSet<Guid> Likes = new();
    protected readonly List<string> Comments = new();
    protected readonly HashSet<Guid> Shares = new();

    public int LikeCount => Likes.Count;
    public int HowManyComment => Comments.Count;
    public int ShareCount => Shares.Count;


    protected Post(Guid authorId, string contentText)
        : base(authorId, contentText)
    {
    }
    
    public virtual void AddLike(Guid userId) => Likes.Add(userId);
    public virtual void RemoveLike(Guid userId) => Likes.Remove(userId);
    public virtual void AddComment(Guid userId, string text) => Comments.Add(text);
    public virtual void AddShare(Guid userId) => Shares.Add(userId);
}        
