using Todo.Models;

namespace ToDoApp.Data.Repositories
{
    public interface IUserRepository
    {
        public Task<int> CreateUser(User user);
        public Task<int> DeleteUser(User user);
    }
}
