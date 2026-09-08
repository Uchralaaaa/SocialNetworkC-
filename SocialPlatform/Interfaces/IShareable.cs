using System;
using System.Collections.Generic;
using System.Text;

namespace SocialPlatform.Interfaces
{
    public interface IShareable
    {
        int ShareCount { get; }
        void AddShare(Guid userId);
    }
}
