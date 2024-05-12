using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Todo.Data;
using Todo.Models;

namespace ToDoApp.Data.Repositories
{
    public class AuthenticateUser : IAuthenticateUser
    {
        private readonly TodoContext todoContext;
        private readonly ILogger<AuthenticateUser> logger;

        public AuthenticateUser(TodoContext todoContext, ILogger<AuthenticateUser> logger)
        {
            this.todoContext = todoContext;
            this.logger = logger;
        }
        async Task<User> IAuthenticateUser.AuthenticateUser(string username, string password)
        {
            var users = await todoContext.User.ToListAsync();
            var user = from u in users
                       where u.Username == username
                       select u;
            IPasswordHasher<User> passwordHasher = new PasswordHasher<User>();
            var result = passwordHasher.VerifyHashedPassword(user.First(), user.First().Password, password);
            if(result != 0)
            {
                return user.First();
            }
            else
            {
                return null;
            }
        }
    }
}
