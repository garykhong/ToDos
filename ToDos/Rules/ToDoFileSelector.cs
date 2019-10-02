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
    public class ToDoFileSelector
    {
        public ToDoFile GetToDoFile(int toDoFileID)
        {
            ToDoFile toDoFile = ToDoDBContextFactory.Create().ToDoFiles.Find(toDoFileID);
            return toDoFile;
        }

        public ToDoFile GetToDoFile(int toDoID, int toDoFileID, string userName)
        {
            ToDo toDoThatIsSaved = new ToDoSelector().GetToDo(toDoID, userName);
            return GetToDoFile(toDoThatIsSaved, toDoFileID);
        }

        public ToDoFile GetToDoFile(ToDo toDoThatIsSaved, int toDoFileID)
        {
            ToDoFile toDoFile = toDoThatIsSaved.ToDoFiles.
                                               Where(toDoFileThatIsSaved => toDoFileThatIsSaved.ID == toDoFileID).
                                                 FirstOrDefault();
            return toDoFile;
        }

        public ToDoFile GetToDoFile(HttpPostedFileBase file, int toDoID)
        {
            ToDoFile toDoFile = new ToDoFile();
            using (BinaryReader binaryReader = new BinaryReader(file.InputStream))
            {
                toDoFile.Data = binaryReader.ReadBytes(file.ContentLength);
                toDoFile.Name = Path.GetFileName(file.FileName);
                toDoFile.ContentType = file.ContentType;
                toDoFile.ToDoID = toDoID;
            }

            return toDoFile;
        }

        public ICollection<ToDoFile> GetToDoFiles(int ID)
        {
            return ToDoDBContextFactory.Create().ToDoFiles.Where(toDoFile => toDoFile.ToDoID == ID).ToList();
        }
    }
}
