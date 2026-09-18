using System;
using System.Collections.Generic;
using System.Linq;
using SocialPlatform.Domain;

namespace SocialPlatform.Repository
{
    public class PostRepository
    {
        private readonly SocialDbContext _context;

        public PostRepository(SocialDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Add(Post post)
        {
            if (post == null) throw new ArgumentNullException(nameof(post));
            _context.Posts.Add(post);
            _context.SaveChanges();
        }

        public Post? GetById(Guid postId)
        {
            return _context.Posts.FirstOrDefault(p => p.Id == postId);
        }

        public IEnumerable<Post> GetAll()
        {
            return _context.Posts.ToList();
        }

        public void Update(Post post)
        {
            if (post == null) throw new ArgumentNullException(nameof(post));
            _context.Posts.Update(post);
            _context.SaveChanges();
        }
    }
}