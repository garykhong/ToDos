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
    public class ToDoFileInserter
    {
        public void InsertFileByLoggedInUserName(HttpPostedFileBase postedFile, int toDoID)
        {
            ToDo toDoToUpdate = new ToDoSelector().GetToDoByLoggedInUserName(toDoID);
            InsertFile(postedFile, toDoID, toDoToUpdate.UserName);           
        }

        public void InsertFile(HttpPostedFileBase postedFile, int toDoID, string userName)
        {
            ToDo toDoToUpdate = new ToDoSelector().GetToDo(toDoID, userName);
            toDoToUpdate.ToDoFiles.Add(new ToDoFileSelector().GetToDoFile(postedFile, toDoID));
            ToDoDBContextFactory.Create().SaveChanges();
        }        

        public void InsertFile(ToDo toDo, ToDoFile toDoFile)
        {
            toDo.ToDoFiles.Add(toDoFile);
            ToDoDBContextFactory.Create().SaveChanges();
        }
    }
}
