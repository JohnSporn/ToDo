
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
            return null;
        }
    }
}
