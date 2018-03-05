using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ToDos.Models;
using Microsoft.AspNet.Identity;

namespace ToDos.Controllers
{
    public class ToDoFileController : Controller
    {
        public ViewResult Index(int? toDoID)
        {
            ToDo toDo = ToDoDBContextFactory.Create().
                ToDos.Where(savedToDo => savedToDo.ID == toDoID).FirstOrDefault();
            return View("Index", toDo);
        }

        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase postedFile, ToDo toDo)
        {
            ToDoFile toDoFile = new ToDoFile();
            using (BinaryReader br = new BinaryReader(postedFile.InputStream))
            {
                toDoFile.Data = br.ReadBytes(postedFile.ContentLength);
                toDoFile.Name = Path.GetFileName(postedFile.FileName);
                toDoFile.ContentType = postedFile.ContentType;
                toDoFile.ToDoID = toDo.ID;
            }
            
            ToDoDBContextFactory.Create().ToDos.Where(savedToDo => savedToDo.ID == toDo.ID).FirstOrDefault().ToDoFiles.Add(toDoFile);
            ToDoDBContextFactory.Create().SaveChanges();

            return View("Index", ToDoDBContextFactory.Create().ToDos.Where(savedToDo => savedToDo.ID == toDo.ID).FirstOrDefault());
        }

        [HttpPost]
        public FileResult DownloadFile(int? fileId, ToDo toDo)
        {
            ToDo toDoThatIsSaved = ToDoDBContextFactory.Create().ToDos.Where(savedToDo => savedToDo.ID == toDo.ID).FirstOrDefault();
            ToDoFile toDoFile = toDoThatIsSaved.ToDoFiles.Where(file => file.ID == fileId).FirstOrDefault();
            return File(toDoFile.Data, toDoFile.ContentType, toDoFile.Name);
        }

        [HttpPost]
        public ActionResult Delete(int toDoFileID, ToDo toDo)
        {
            ToDo toDoThatIsSaved = ToDoDBContextFactory.Create().ToDos.Where(savedToDo => savedToDo.ID == toDo.ID).FirstOrDefault();
            ToDoFile toDoFileToBeRemoved = toDoThatIsSaved.ToDoFiles.Where(toDoFile => toDoFile.ID == toDoFileID).FirstOrDefault();
            toDoThatIsSaved.ToDoFiles.Remove(toDoFileToBeRemoved);
            ToDoDBContextFactory.Create().ToDoFiles.Remove(toDoFileToBeRemoved);
            ToDoDBContextFactory.Create().SaveChanges();
            return RedirectToAction("Index", new { toDoID = toDo.ID });
        }
    }
}
