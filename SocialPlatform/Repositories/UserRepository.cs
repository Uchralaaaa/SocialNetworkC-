using Microsoft.EntityFrameworkCore;
using SocialPlatform.Domain;
using SocialPlatform.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SocialPlatform
{
    public class UserRepository
    {
        private readonly SocialDbContext _context;

        // Constructor-оор DbContext-ээ хүлээж авна
        public UserRepository(SocialDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // Default constructor (байхгүй бол тохируулж өгнө)
        public UserRepository()
        {
            var options = new DbContextOptionsBuilder<SocialDbContext>()
                .UseSqlite("Data Source=social.db")
                .Options;

            _context = new SocialDbContext(options);
            _context.Database.EnsureCreated();
        }

        public void Add(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            _context.Users.Add(user);
            _context.SaveChanges(); // Бааз руу бодитоор хадгална
        }

        public User? GetById(Guid userId)
        {
            // LINQ ашиглан SQLite баазаас хайна
            return _context.Users.FirstOrDefault(u => u.UserId == userId);
        }

        public User? GetByUsername(string username)
        {
            // Нэвтрэх болон бүртгэлд зориулж Username-ээр хайх
            return _context.Users.FirstOrDefault(u => u.UserName.ToLower() == username.ToLower());
        }

        public IEnumerable<User> GetAll()
        {
            // Баазад байгаа бүх хэрэглэгчийг жагсаалт болгож буцаана
            return _context.Users.ToList();
        }

        public void Delete(Guid userId)
        {
            var user = GetById(userId);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges(); // Баазаас устгаж хадгална
            }
        }
    }
}