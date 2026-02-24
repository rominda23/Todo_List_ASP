using Todo_List_ASP.DTOs;
using Todo_List_ASP.Models;

//(interface) = the contract (what functions exist)
namespace Todo_List_ASP.Services
{
    public interface ITodoService
    {
        Task<List<TodoItem>> GetAllTasksAsync();
        Task<TodoItem> CreateTaskAsync(CreateTaskRequest request);
    }
}
