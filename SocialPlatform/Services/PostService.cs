using System;
using System.Collections.Generic;
using System.Text;
using SocialPlatform.Repository;
using SocialPlatform.Domain;
using System.Runtime.CompilerServices;

namespace SocialPlatform.Services
{
    public class PostService
    {
        private readonly PostRepository _postRepository;

        public PostService(PostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public TextPost CreateTextPost(Guid authorId, string contentText)
        {
            var newPost = new TextPost(authorId, contentText);
            _postRepository.Add(newPost);
            return newPost;
        }

        public ImagePost CreateImagePost(Guid authorId, string contentText, string imageURL) 
        {
            var newPost = new ImagePost(authorId, contentText, imageURL);
            
            _postRepository.Add(newPost);
            return newPost;
        }

        public Post? GetPost(Guid postId)
        {
            return _postRepository.GetById(postId);
        }

        //interaction buyu like, comment, repost
        public bool LikePost(Guid postId, Guid userId)
        {
            var post = _postRepository.GetById(postId);
            if (post == null) return false;

            post.AddLike(userId);
            return true;
        }

        public bool CommentOnPost(Guid postId, Guid userId, string commentText)
        {
            var post = _postRepository.GetById(postId);
            if (post == null) return false;

            post.AddComment(userId, commentText);
            return true;
        }

        public bool SharePost(Guid postId, Guid userId)
        {
            var post = _postRepository.GetById(postId);
            if (post == null) return false;

            post.AddShare(userId);
            return true;
        }

        public IEnumerable<Post> GetAllPosts()
        {
            return _postRepository.GetAll();
        }
    }
}
