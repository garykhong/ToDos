using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDos.Models;

namespace ToDos.Rules
{
    public class ToDoReminderDeletor
    {
        public void DeleteToDoReminder(int id)
        {
            ToDoReminder toDoReminderToBeDeleted = ToDoDBContextFactory.Create().
                ToDoReminders.Where(toDoReminder => toDoReminder.ID == id).FirstOrDefault();
            ToDoDBContextFactory.Create().ToDoReminders.Remove(toDoReminderToBeDeleted);
            ToDoDBContextFactory.Create().SaveChanges();
        }
    }
}
