using Todo.Models;

namespace ToDoApp.Data.Repositories
{
    public interface IAuthenticateUser
    {
        public Task<User> AuthenticateUser(string username, string password);
    }
}
