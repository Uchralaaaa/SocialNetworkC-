using System;
using System.Collections.Generic;
using System.Text;
using SocialPlatform.Domain;

namespace SocialPlatform.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User RegisterUser(string username, DateTime DOB, string password, string profilePic)
        {
            //user entity uusgeh entity gej yuve 
            var newUser = new User(username, DOB, password, profilePic);
            
            //shine user nemeh
            _userRepository.Add(newUser);

            return newUser;
        }

        public User? GetUser(Guid userId) { 
            return _userRepository.GetById(userId);
        }
    }
}
