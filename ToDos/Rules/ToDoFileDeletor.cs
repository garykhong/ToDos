using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDos.Models;

namespace ToDos.Rules
{
    public class ToDoFileDeletor
    {
        public void DeleteToDoFileByLoggedInUserName(int toDoFileID)
        {
            ToDoFile toDoFile = new ToDoFileSelector().GetToDoFile(toDoFileID);
            string userName = new LoggedInUserFinder().GetUserName();
            DeleteToDoFile(toDoFileID, toDoFile.ToDoID, userName);
        }

        public void DeleteToDoFile(int toDoFileID, int toDoID, string userName)
        {
            ToDo toDoThatIsSaved = new ToDoSelector().GetToDo(toDoID, userName);
            ToDoFile toDoFileToBeRemoved = new ToDoFileSelector().GetToDoFile(toDoThatIsSaved, toDoFileID);
            toDoThatIsSaved.ToDoFiles.Remove(toDoFileToBeRemoved);
            ToDoDBContextFactory.Create().ToDoFiles.Remove(toDoFileToBeRemoved);
            ToDoDBContextFactory.Create().SaveChanges();
        }
    }
}
