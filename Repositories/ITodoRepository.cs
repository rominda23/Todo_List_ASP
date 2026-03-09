using Todo_List_ASP.Models;

namespace Todo_List_ASP.Repositories
{
    public interface ITodoRepository
    {
        Task<List<TodoItem>> GetAllAsync();
        Task<TodoItem> AddAsync(TodoItem item);
    }
}
