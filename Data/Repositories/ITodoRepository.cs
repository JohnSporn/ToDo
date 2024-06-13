using Todo.Models;

namespace ToDoApp.Data.Repositories
{
    public interface ITodoRepository
    {
        public Task<int> CreateTodo(ToDo toDo);
        public Task<int> UpdateTodo(ToDo toDo);
        public Task<int> DeleteTodo(ToDo toDo);
        public IAsyncEnumerable<ToDo> GetToDos();
    }
}
