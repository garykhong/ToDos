using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDos.Models;

namespace ToDos.Rules
{
    public class ToDoUpdater
    {
        public void UpdateToDo(ToDo toDo)
        {
            ToDoDBContextFactory.Create().SetToDoEntryState(toDo);
            toDo.ToDoFiles = ToDoDBContextFactory.Create().ToDoFiles.Where(toDoFile => toDoFile.ToDoID == toDo.ID).ToList();
            ToDoDBContextFactory.Create().SaveChanges();
        }
    }
}
