using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDos.Models;

namespace ToDos.Rules
{
    public class ToDoInserter
    {
        public void SaveToDoWithLoggedInUserName(ToDo toDo)
        {
            SetUserName(toDo);
            SetToDoFiles(toDo);
            SetToDoReminders(toDo);
            SetOrderID(toDo);
            InsertToDoToDatabase(toDo);
        }

        private void SetUserName(ToDo toDo)
        {
            toDo.UserName = new LoggedInUserFinder().GetUserName();
        }

        private void SetToDoFiles(ToDo toDo)
        {
            if (toDo.ToDoFiles == null)
            {
                toDo.ToDoFiles = new List<ToDoFile>();
            }
        }

        private void SetToDoReminders(ToDo toDo)
        {
            if (toDo.ToDoReminders == null)
            {
                toDo.ToDoReminders = new List<ToDoReminder>();
            }
        }

        private void SetOrderID(ToDo toDo)
        {
            if (toDo.OrderID == 0)
            {
                toDo.OrderID = GetMaxPlusOneToDoOrderID(toDo.UserName);
            }
        }

        private int GetMaxPlusOneToDoOrderID(string userName)
        {
            return new ToDoSelector().GetMaxPlusOneToDoOrderID(userName);
        }

        private void InsertToDoToDatabase(ToDo toDo)
        {
            ToDoDBContextFactory.Create().ToDos.Add(toDo);
            ToDoDBContextFactory.Create().SaveChanges();
        }
    }
}
