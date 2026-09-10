using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
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

        public User? GetById(Guid UserId)
        {
            foreach (var user in _users)
            {
                if(user.UserId == UserId)
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

        public void Delete(Guid UserId)
        {
            var user = GetById(UserId);
            if(user != null)
            {
                _users.Remove(user);
            }
        }
    }   
}
