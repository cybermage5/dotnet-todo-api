namespace dotnet_todo_api.src.TodoApi.Models
{
    public class SignUpModel
    {
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
