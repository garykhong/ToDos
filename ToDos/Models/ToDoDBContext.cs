using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;

namespace ToDos.Models
{
    public class ToDoDBContext : DbContext
    {
        public virtual IDbSet<ToDo> ToDos { get; set; }
        public virtual IDbSet<ToDoFile> ToDoFiles { get; set; }
        public virtual void SetToDoEntryState(ToDo toDo)
        {
            this.Set<ToDo>().AddOrUpdate(toDo);
        }
    }
}