using System;
using System.Collections.Generic;
using System.Text;

namespace SocialPlatform
{
    public interface ILikeable
    {
        int LikeCount { get; }
        void AddLike(Guid userId);
        void RemoveLike(Guid userId);
    }
}
