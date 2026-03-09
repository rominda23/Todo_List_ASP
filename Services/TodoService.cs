using Todo_List_ASP.DTOs;
using Todo_List_ASP.Models;
using Todo_List_ASP.Repositories;

namespace Todo_List_ASP.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _repo;

        public TodoService(ITodoRepository repo) // ← inject repo, not DbContext
        {
            _repo = repo;
        }

        public async Task<List<TodoItem>> GetAllTasksAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<TodoItem> CreateTaskAsync(CreateTaskRequest request)
        {
            var task = new TodoItem { InputTask = request.InputTask };
            return await _repo.AddAsync(task);
        }
    }
}