using Todo_List_ASP.DTOs;
using Todo_List_ASP.Models;
using Todo_List_ASP.Repositories;

namespace Todo_List_ASP.Services
{
    // Services/TodoService.cs
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _repo;
        private readonly IHttpContextAccessor _http;

        public TodoService(ITodoRepository repo, IHttpContextAccessor http)
        {
            _repo = repo;
            _http = http;
        }

        private int GetUserId()
        {
            var claim = _http.HttpContext!.User
                .FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);

            if (claim == null) throw new UnauthorizedAccessException();
            return int.Parse(claim.Value);
        }

        public async Task<List<TodoItem>> GetAllTasksAsync()
        {
            var userId = GetUserId();
            return await _repo.GetAllByUserAsync(userId); // new method below
        }

        public async Task<TodoItem> CreateTaskAsync(CreateTaskRequest request)
        {
            var task = new TodoItem
            {
                InputTask = request.InputTask,
                UserId = GetUserId(),            // stamp the owner
                CreatedAt = DateTime.Now
            };
            return await _repo.AddAsync(task);
        }
    }
}