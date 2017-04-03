using System.Data.Entity;

namespace ToDos.Models
{
    public class ToDoDBContext : DbContext
    {
        public DbSet<ToDo> ToDos { get; set; }
    }
}