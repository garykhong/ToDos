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
            toDoToUpdate.ToDoFiles.Add(GetToDoFile(postedFile, toDoID));
            ToDoDBContextFactory.Create().SaveChanges();
        }

        private ToDoFile GetToDoFile(HttpPostedFileBase file, int toDoID)
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
    }
}
