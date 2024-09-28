using dotnet_todo_api.src.TodoApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace dotnet_todo_api.src.TodoApi.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<IdentityUser>(options)
    {
        public DbSet<Todo> Todos { get; set; }  // Add this line to manage Todo entities
    }
}