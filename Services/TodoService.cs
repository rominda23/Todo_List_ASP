using Microsoft.EntityFrameworkCore;
using Todo_List_ASP.Data;
using Todo_List_ASP.DTOs;
using Todo_List_ASP.Models;

//Service = does the “work” (create task, mark complete, rules, etc.)
namespace Todo_List_ASP.Services
{
    public class TodoService : ITodoService
    {
        private readonly AppDbContext _db;

        public TodoService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<TodoItem>> GetAllTasksAsync()
        {
            return await _db.TodoItems
                            .OrderBy(t => t.Id)
                            .ToListAsync();
        }

        public async Task<TodoItem> CreateTaskAsync(CreateTaskRequest request)
        {
            var task = new TodoItem
            {
                InputTask = request.InputTask
            };
            _db.TodoItems.Add(task);
            await _db.SaveChangesAsync();
            return task;
        }
    }
}
