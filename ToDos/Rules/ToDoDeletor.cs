using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDos.Models;

namespace ToDos.Rules
{
    public class ToDoDeletor
    {
        public void DeleteToDo(int id)
        {
            ToDo toDoToBeDeleted = ToDoDBContextFactory.Create().
                ToDos.Where(toDo => toDo.ID == id).FirstOrDefault();
            new ToDoReminderDeletor().DeleteToDoReminders(toDoToBeDeleted);
            new ToDoFileDeletor().DeleteToDoFiles(toDoToBeDeleted);
            ToDoDBContextFactory.Create().ToDos.Remove(toDoToBeDeleted);
            ToDoDBContextFactory.Create().SaveChanges();
        }
    }
}
