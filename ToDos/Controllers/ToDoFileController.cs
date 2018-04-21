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
using ToDos.Rules;

namespace ToDos.Controllers
{
    public class ToDoFileController : Controller
    {
        public ViewResult Index(int? toDoID)
        {
            ToDo toDo = new ToDoSelector().GetToDoByLoggedInUserName(toDoID);
            return View(nameof(Index), toDo);
        }

        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase postedFile, ToDo toDo)
        {
            new ToDoFileInserter().InsertFileByLoggedInUserName(postedFile, toDo.ID);
            return View(nameof(Index), new ToDoSelector().GetToDoByLoggedInUserName(toDo.ID));
        }

        [HttpPost]
        public FileResult DownloadFile(int? fileId, ToDo toDo)
        {
            ToDo toDoThatIsSaved = new ToDoSelector().GetToDoByLoggedInUserName(toDo.ID);
            ToDoFile toDoFile = toDoThatIsSaved.ToDoFiles.Where(file => file.ID == fileId).FirstOrDefault();
            return File(toDoFile.Data, toDoFile.ContentType, toDoFile.Name);
        }

        [HttpPost]
        public ActionResult Delete(int toDoFileID, ToDo toDo)
        {
            new ToDoFileDeletor().DeleteToDoFileByLoggedInUserName(toDoFileID);
            return RedirectToAction(nameof(Index), new { toDoID = toDo.ID });
        }
    }
}
