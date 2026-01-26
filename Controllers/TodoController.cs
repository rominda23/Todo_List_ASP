using Microsoft.AspNetCore.Mvc;
using Todo_List_ASP.Models; //call the namespace of your Model(Getter & Setter) to use it here in Controller

namespace Todo_List_ASP.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private static readonly List<TodoItem> Items = new()
        {
            new TodoItem { Id = 1, Title = "Learn ASP.NET Core", IsDone = false}
        };

        [HttpGet]
        public ActionResult<List<TodoItem>> GetAll() => Items;

        [HttpPost]
        public ActionResult<TodoItem> Add(TodoItem item)
        {
            item.Id = Items.Count == 0 ? 1 : Items.Max(x => x.Id) + 1;
            Items.Add(item);
            return CreatedAtAction(nameof(GetAll), new { id = item.Id }, item);
        }
    }
}
