using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;
using SocialPlatform.Domain;

namespace SocialPlatform.Repository
{
    public class PostRepository
    {
        private readonly List<Post> _posts = new List<Post>();

        public void Add(Post post)
        {
            _posts.Add(post);
        }

        public Post? GetById(Guid Id)
        {
            foreach(var post in _posts)
            {
                if(post.ContentId == Id)
                {
                    return post;
                }
            }
            return null;
        }

        public IEnumerable<Post> GetAll() => _posts;

        public void Delete(Guid ContentId)
        {
            var post = GetById(ContentId);
            if (post != null) { _posts.Remove(post); }
        }
    }
}
