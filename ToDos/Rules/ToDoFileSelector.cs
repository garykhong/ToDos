using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDos.Models;

namespace ToDos.Rules
{
    public class ToDoFileSelector
    {
        public ToDoFile GetToDoFile(int toDoFileID)
        {
            ToDoFile toDoFile = ToDoDBContextFactory.Create().ToDoFiles.Find(toDoFileID);
            return toDoFile;
        }

        public ToDoFile GetToDoFile(ToDo toDoThatIsSaved, int toDoFileID)
        {
            ToDoFile toDoFile = toDoThatIsSaved.ToDoFiles.
                                               Where(toDoFileThatIsSaved => toDoFileThatIsSaved.ID == toDoFileID).
                                                 FirstOrDefault();
            return toDoFile;
        }
    }
}
