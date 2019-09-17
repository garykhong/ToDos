using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDos.Models;

namespace ToDos.Rules
{
    public class ToDoReminderUpdater
    {
        public void UpdateToDoReminder(ToDoReminder toDoReminder)
        {
            ToDoDBContextFactory.Create().SetToDoReminderEntryState(toDoReminder);            
            ToDoDBContextFactory.Create().SaveChanges();
        }
    }
}
