using System.Data.Entity;

namespace ToDos.Models
{
    public class ToDoDBContext : DbContext
    {
        public IDbSet<ToDo> ToDos { get; set; }
    }
}