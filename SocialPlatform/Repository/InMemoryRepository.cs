using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Linq;
using SocialPlatform.Interfaces;
using SocialPlatform.Domain;

namespace SocialPlatform
{
    public class UserRepository
    {
        private readonly List<User> _users = new List<User>();

        public void Add(User user)
        {
            _users.Add(user);
        }

        public User? GetById(Guid userId)
        {
            foreach (var user in _users)
            {
                if(user.UserId == userId)
                {
                    return user;
                }
            }
            return null;
        }

        public IEnumerable<User> GetAll()
        {
            return _users;
        }

        public void Delete(Guid userId)
        {
            var user = GetById(userId);
            if(user != null)
            {
                _users.Remove(user);
            }
        }
    }   
}
