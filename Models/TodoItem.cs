namespace Todo_List_ASP.Models
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string InputTask { get; set; } = "";

        public int UserId { get; set; }
        public User User { get; set; } = null!;

    }
}
