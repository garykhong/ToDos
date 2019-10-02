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
        public const string MaintainFiles = "Maintain files";
        public const string MaintainReminders = "Maintain reminders";
        private UserFinder loggedInUserFinder;

        public ToDoController()
        {
            this.loggedInUserFinder = new LoggedInUserFinder();
        }

        public ToDoController(UserFinder loggedInUserFinder)
        {
            this.loggedInUserFinder = loggedInUserFinder;
        }

        public ViewResult Index()
        {
            string userName = loggedInUserFinder.GetUserName();            
            return View(nameof(Index), new ToDoSelector().GetSortedToDosByLoggedInUserName(userName));
        }

        public ViewResult GetToDosAfterUpdatingReminders()
        {
            string userName = loggedInUserFinder.GetUserName();
            new ToDoReminderController().MoveDueToDosToLastPriority();
            return View(nameof(Index), new ToDoSelector().GetSortedToDosByLoggedInUserName(userName));
        }

        public ViewResult FilterByWhatToDo(string whatToDoContainsThis)
        {
            if (whatToDoContainsThis == string.Empty)
            {
                return Index();
            }

            string userName = loggedInUserFinder.GetUserName();

            var toDosFound = new ToDoSelector().GetToDosThatIsLikeWhatToDo(whatToDoContainsThis, userName);
            return View(nameof(Index), toDosFound);
        }        

        public ViewResult Create()
        {
            return View(nameof(Create), new ToDo());
        }

        [HttpPost]
        public ActionResult Create(ToDo toDo, string maintainToDoChild)
        {            
            switch(maintainToDoChild)
            {
                case MaintainFiles:
                    return RedirectToAction(nameof(ToDoFileController.Index), nameof(ToDoFile), toDo);
                case MaintainReminders:
                    return RedirectToAction(nameof(ToDoReminderController.Index), nameof(ToDoReminder), new { toDoID = toDo.ID });
                default:
                    new ToDoInserter().SaveToDoWithLoggedInUserName(toDo);
                    return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public ActionResult Edit(int? toDoID)
        {
            if (toDoID == null || toDoID == 0)
                return RedirectToAction(nameof(this.Create));

            return View(nameof(Edit),
                new ToDoSelector().GetToDo(toDoID, loggedInUserFinder.GetUserName()));
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