using Microsoft.EntityFrameworkCore;
using ToDoService.Model;

namespace ToDoService.Data
{
    public class ToDoDbContext : DbContext
    {
        public ToDoDbContext(DbContextOptions<ToDoDbContext> options) : base(options) 
        {
            
        }
        public DbSet<ToDoModel> Tasks { get; set; }
    }
}
