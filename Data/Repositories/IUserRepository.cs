using Todo.Models;

namespace ToDoApp.Data.Repositories
{
    public interface IUserRepository
    {
        public Task<int> CreateUser(User user);
        public Task<int> UpdateUser(User user);
        public IAsyncEnumerable<User> GetUsers();
        public Task<int> DeleteUser(User user);
    }
}
