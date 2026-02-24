using Microsoft.AspNetCore.Components.Forms;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todo_List_ASP.Data;
using Todo_List_ASP.Models; //call the namespace of your Model(Getter & Setter) to use it here in Controller
using Todo_List_ASP;
using Todo_List_ASP.DTOs;
using Todo_List_ASP.Services;

//Controller = handles HTTP stuff (routes, status codes, validation, request/response)

namespace Todo_List_ASP.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        //    private static readonly List<TodoItem> _tasks = new();
        //    private static int _nextId = 1;

        //    [HttpGet]
        //    public ActionResult<List<TodoItem>> Get()
        //    {
        //        return Ok(_tasks);
        //    }

        //    public class CreateTaskRequest
        //    {
        //        public string InputTask { get; set; } = "";
        //    }

        //    [HttpPost]
        //    public ActionResult<TodoItem> Post([FromBody] CreateTaskRequest request)
        //    {
        //        if (string.IsNullOrWhiteSpace(request.InputTask))
        //            return BadRequest("InputTask is required.");

        //        var task = new TodoItem
        //        {
        //            Id = _nextId++,
        //            InputTask = request.InputTask
        //        };

        //        _tasks.Add(task);
        //        return Ok(task);
        //    }
        //}

        //private readonly AppDbContext _db;
        private readonly ITodoService _service;

        public TodoController(ITodoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<TodoItem>>> Get()
        {
            var items = await _service.GetAllTasksAsync();
            return Ok(items);
        }

       
        [HttpPost]
        public async Task<ActionResult<TodoItem>> Post([FromBody] CreateTaskRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.InputTask))
                return BadRequest("InputTask is required.");

            var created = await _service.CreateTaskAsync(request);
            return Ok(created);
        }
    }
}
