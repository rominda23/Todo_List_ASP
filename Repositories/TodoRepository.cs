using Microsoft.EntityFrameworkCore;
using Todo_List_ASP.Data;
using Todo_List_ASP.Models;

namespace Todo_List_ASP.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly AppDbContext _db;

        public TodoRepository(AppDbContext db)
        {
            _db = db;
        }

        
        public async Task<List<TodoItem>> GetAllByUserAsync(int userId)
        {
            return await _db.TodoItems
                            .Where(t => t.UserId == userId)
                            .OrderBy(t => t.Id)
                            .ToListAsync();
        }

        public async Task<TodoItem> AddAsync(TodoItem item)
        {
            _db.TodoItems.Add(item);
            await _db.SaveChangesAsync();
            return item;
        }
    }
}
