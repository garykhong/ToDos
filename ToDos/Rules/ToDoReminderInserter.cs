using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ToDos.Models;

namespace ToDos.Rules
{
    public class ToDoReminderInserter
    {
        public void InsertReminderByLoggedInUserName(ToDoReminder toDoReminder)
        {
            ToDo toDoToUpdate = new ToDoSelector().GetToDoByLoggedInUserName(toDoReminder.ToDoID);
            toDoToUpdate.ToDoReminders.Add(toDoReminder);
            ToDoDBContextFactory.Create().SaveChanges();
        }
    }
}
