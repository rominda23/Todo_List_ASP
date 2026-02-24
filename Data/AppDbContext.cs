using Microsoft.EntityFrameworkCore;
using Todo_List_ASP.Models;


//DbContext/DB = stores data
namespace Todo_List_ASP.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<TodoItem> TodoItems => Set<TodoItem>();
    }
}