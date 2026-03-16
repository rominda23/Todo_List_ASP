using Todo_List_ASP.Models;

namespace Todo_List_ASP.Repositories
{
    public interface ITodoRepository
    { 
        Task<TodoItem> AddAsync(TodoItem item);
        Task<List<TodoItem>> GetAllByUserAsync(int userId);  // add this
    }
}
