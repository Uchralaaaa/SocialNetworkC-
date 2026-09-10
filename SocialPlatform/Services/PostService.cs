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
    }
}
