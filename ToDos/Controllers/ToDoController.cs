using System;
using System.Web.Mvc;
using ToDos.Models;
using System.Linq;
using ToDos.Controllers.Attributes;
using System.Collections.Generic;
using ToDos.Rules;

namespace ToDos.Controllers
{
    [RequireHttpsForRemoteRequest]
    [Authorize]
    public class ToDoController : Controller
    {
        public ViewResult Index()
        {
            string userName = new LoggedInUserFinder().GetLoggedInUserName();
            return View(nameof(Index), new ToDoSelector().GetSortedToDosByLoggedInUserName(userName));
        }        

        public ViewResult FilterByWhatToDo(string whatToDoContainsThis)
        {
            if (whatToDoContainsThis == string.Empty)
            {
                return Index();
            }

            string userName = new LoggedInUserFinder().GetLoggedInUserName();

            var toDosFound = new ToDoSelector().GetToDosThatIsLikeWhatToDo(whatToDoContainsThis, userName);
            return View(nameof(Index), toDosFound);
        }

        public ViewResult Details(int? toDoID)
        {
            
            ToDo toDo = new ToDoSelector().GetToDoByLoggedInUserName(toDoID);           
            return View(nameof(Details), toDo);
        }

        public ViewResult Create()
        {
            return View(nameof(Create));
        }

        [HttpPost]
        public ActionResult Create(ToDo toDo, string maintainFiles)
        {
            new ToDoInserter().SaveToDoWithLoggedInUserName(toDo);
            if (!string.IsNullOrEmpty(maintainFiles))
            {
                return RedirectToAction(nameof(Index), nameof(ToDoFile), new { toDoID = toDo.ID });
            }
            return RedirectToAction(nameof(Index));
        }        

        [HttpGet]
        public ViewResult Edit(int? toDoID)
        {            
            return View(nameof(Edit),
                new ToDoSelector().GetToDoByLoggedInUserName(toDoID));
        }

        [HttpPost]
        public ActionResult Edit(ToDo toDo)
        {
            ResetToDoDBContext();
            new ToDoUpdater().UpdateToDo(toDo);
            return RedirectToAction(nameof(Index));
        }

        protected virtual void ResetToDoDBContext()
        {
            new ToDoDBContextResetter().ResetToDoDBContext();
        }
    }
}