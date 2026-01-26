namespace Todo_List_ASP.Models
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public bool IsDone { get; set; }
    }
}
