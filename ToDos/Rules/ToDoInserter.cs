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
            SetOrderID(toDo);
            InsertToDoToDatabase(toDo);
        }

        private void SetUserName(ToDo toDo)
        {
            toDo.UserName = new LoggedInUserFinder().GetLoggedInUserName();
        }

        private void SetToDoFiles(ToDo toDo)
        {
            if (toDo.ToDoFiles == null)
            {
                toDo.ToDoFiles = new List<ToDoFile>();
            }
        }

        private void SetOrderID(ToDo toDo)
        {
            if (toDo.OrderID == 0)
            {
                toDo.OrderID = GetMaxToDoOrderID() + 1;
            }
        }

        private int GetMaxToDoOrderID()
        {
            IDbSet<ToDo> toDos = ToDoDBContextFactory.Create().ToDos;
            return toDos.Count() > 0 ? toDos.Max(td => td.OrderID) : 0;
        }

        private void InsertToDoToDatabase(ToDo toDo)
        {
            ToDoDBContextFactory.Create().ToDos.Add(toDo);
            ToDoDBContextFactory.Create().SaveChanges();
        }
    }
}
