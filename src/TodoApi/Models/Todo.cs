namespace dotnet_todo_api.src.TodoApi.Models
{
    public class Todo
    {
        public long Id { get; set; }
        public required string Title { get; set; }
        public bool IsComplete { get; set; }
    }
}