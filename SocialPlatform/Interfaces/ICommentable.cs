using System;
using System.Collections.Generic;
using System.Text;

namespace SocialPlatform.Interfaces
{
    public interface ICommentable
    {
        int HowManyComment { get; }
        void AddComment(Guid authorId, string text);
    }
}
