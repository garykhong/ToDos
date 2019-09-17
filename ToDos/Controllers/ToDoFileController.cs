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
using ToDos.Controllers.Attributes;

namespace ToDos.Controllers
{
    [RequireHttpsForRemoteRequest]
    [Authorize]
    public class ToDoFileController : Controller
    {
        public ViewResult Index(ToDo toDo)
        {
            ToDo toDoToUseAsModel = GetToDoToUseAsModel(toDo);

            return View(nameof(Index), toDoToUseAsModel);
        }

        private ToDo GetToDoToUseAsModel(ToDo toDo)
        {
            return new ToDoSelector().GetToDoToUseAsModel(toDo);
        }

        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase postedFile, ToDo toDo)
        {
            if (toDo.ID == 0)
                new ToDoInserter().SaveToDoWithLoggedInUserName(toDo);
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
