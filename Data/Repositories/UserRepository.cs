using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Todo.Data;
using Todo.Models;

namespace ToDoApp.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ILogger<UserRepository> _logger;
        private readonly TodoContext _context;
        public UserRepository(ILogger<UserRepository> logger, TodoContext context)
        {
            _logger = logger;
            _context = context;
        }
        public async Task<int> CreateUser(User user)
        {
            IPasswordHasher<User> passwordHasher = new PasswordHasher<User>();
            user.Password = passwordHasher.HashPassword(user, user.Password);
            _context.User.Add(user);
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException d)
            {
                _logger.LogError(d.Message);
                return 0;
            }
        }

        public async Task<int> DeleteUser(User user)
        {
            _context.User.Remove(user);
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException d)
            {
                _logger.LogError(d.Message);
                return 0;
            }
        }
    }
}
