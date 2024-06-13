using Microsoft.EntityFrameworkCore;
using Todo.Data;
using Todo.Models;

namespace ToDoApp.Data.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoContext _context;
        private readonly ILogger<TodoRepository> _logger;

        public TodoRepository(TodoContext context, ILogger<TodoRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<int> CreateTodo(ToDo toDo)
        {
            await _context.Todo.AddAsync(toDo);
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException d)
            {
                _logger.LogError(d.Message);
                return 0;
            }
        }

        public async Task<int> UpdateTodo(ToDo toDo)
        {
            _context.Entry(toDo).State = EntityState.Modified;
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

        public async Task<int> DeleteTodo(ToDo toDo)
        {
            _context.Todo.Remove(toDo);
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

        public async IAsyncEnumerable<ToDo> GetToDos()
        {
            IAsyncEnumerable<ToDo> todos = AsyncEnumerable.Empty<ToDo>();
            try
            {
                todos = _context.Todo.AsAsyncEnumerable();
            }
            catch(DbUpdateConcurrencyException d)
            {
                _logger.LogError(d.Message);
            }
            await foreach (var item in todos)
            {
                yield return item;
            }
        }
    }
}
