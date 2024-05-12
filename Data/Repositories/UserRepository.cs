using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Todo.Data;
using Todo.Models;

namespace ToDoApp.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ILogger<UserRepository> logger;
        private readonly TodoContext todoContext;
        public UserRepository(ILogger<UserRepository> logger, TodoContext todoContext)
        {
            this.logger = logger;
            this.todoContext = todoContext;
        }
        public async Task<int> CreateUser(User user)
        {
            IPasswordHasher<User> passwordHasher = new PasswordHasher<User>();
            user.Password = passwordHasher.HashPassword(user, user.Password);
            todoContext.User.Add(user);
            try
            {
                return await todoContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public Task<int> DeleteUser(User user)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<User> GetUsers()
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
