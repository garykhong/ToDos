using System;
using System.Collections.Generic;
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
            toDo.UserName = new LoggedInUserFinder().GetLoggedInUserName();
            if (toDo.ToDoFiles == null)
            {
                toDo.ToDoFiles = new List<ToDoFile>();
            }
            ToDoDBContextFactory.Create().ToDos.Add(toDo);
            ToDoDBContextFactory.Create().SaveChanges();
        }
    }
}
