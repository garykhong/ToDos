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
        public void DeleteToDoReminder(int id, ToDo toDo)
        {
            ToDo toDoThatIsSaved = new ToDoSelector().GetToDo(toDo.ID, toDo.UserName);
            ToDoReminder toDoReminder = new ToDoReminderSelector().GetToDoReminder(toDoThatIsSaved, id);
            toDoThatIsSaved.ToDoReminders.Remove(toDoReminder);            
            ToDoDBContextFactory.Create().ToDoReminders.Remove(toDoReminder);
            ToDoDBContextFactory.Create().SaveChanges();
        }

        public void DeleteToDoReminders(ToDo toDo)
        {
            foreach (ToDoReminder toDoReminder in new ToDoReminderSelector().GetToDoReminders(toDo.ID))
            {
                DeleteToDoReminder(toDoReminder.ID, toDo);
            }
        }
    }
}
