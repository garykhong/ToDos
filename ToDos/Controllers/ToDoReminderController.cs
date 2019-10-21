using System;
using System.Collections.Generic;
using System.Linq;
using ToDos.Models;
using ToDos.Rules;
using System.Web.Mvc;

namespace ToDos.Controllers
{
    public class ToDoReminderController : Controller
    {
        private UserFinder loggedInUserFinder;

        public ToDoReminderController()
        {
            this.loggedInUserFinder = new LoggedInUserFinder();
        }

        public ViewResult Index(ToDo toDo)
        {
            return View(nameof(Index), GetToDoToUseAsModel(toDo));
        }

        private ToDo GetToDoToUseAsModel(ToDo toDo)
        {
            return new ToDoSelector().GetToDoToUseAsModel(toDo);
        }

        public ViewResult Create(int? toDoID)
        {
            if(toDoID != null)
            {
                return View(nameof(Create), GetNewToDoReminder(toDoID));
            }

            return View(nameof(Create), new ToDoReminder());
        }

        [HttpPost]
        public ViewResult Create(ToDoReminder toDoReminder)
        {
            if (toDoReminder.ToDoID == 0)                
            {
                ToDo toDo = new ToDo
                {
                    ToDoReminders = new List<ToDoReminder> { toDoReminder }
                };
                new ToDoInserter().SaveToDoWithLoggedInUserName(toDo);
            }
            else
            {
                new ToDoReminderInserter().InsertReminderByLoggedInUserName(toDoReminder);
            }                
            
            return View(nameof(Index), new ToDoSelector().GetToDoByLoggedInUserName(toDoReminder.ToDoID));
        }

        private ToDoReminder GetNewToDoReminder(int? toDoID)
        {
            return new ToDoReminder { FirstReminderDate = new DateSelector().GetTodaysDateInAustraliaAtMidnight(), ToDoID = (int)toDoID };
        }

        public ActionResult MoveDueToDosToLastPriority()
        {
            string userName = new LoggedInUserFinder().GetUserName();
            DateTime todaysDate = new DateSelector().GetTodaysDateInAustraliaAtMidnight();
            MoveDueToDosToLastPriority(userName, todaysDate);
            return RedirectToAction(nameof(ToDoController.Index), nameof(ToDo));
        }

        public void MoveDueToDosToLastPriority(string userName, DateTime todaysDate)
        {
            if(!string.IsNullOrEmpty(userName))
            {
                foreach(ToDo toDoDueToday in new ToDoSelector().GetToDosDueToday(userName, todaysDate))
                {
                    new ToDoUpdater().MoveToDoDownToLastInPriorityByToDo(toDoDueToday.ID, toDoDueToday.UserName);
                }

                foreach(ToDoReminder toDoReminder in new ToDoReminderSelector().GetToDoRemindersDueToday(userName, todaysDate))
                {
                    toDoReminder.FirstReminderDate = new ToDoReminderSelector().GetNextReminderDueDate(todaysDate, toDoReminder.FrequencyType);
                    new ToDoReminderUpdater().UpdateToDoReminder(toDoReminder);
                }
            }
        }

        [HttpGet]
        public ActionResult Edit(int? toDoReminderID)
        {
            if (toDoReminderID == null || toDoReminderID == 0)
                return RedirectToAction(nameof(this.Create));

            return View(nameof(Edit),
                          new ToDoReminderSelector().GetToDoReminder(toDoReminderID.Value));
        }

        [HttpPost]
        public ActionResult Edit(ToDoReminder toDoReminder)
        {
            new ToDoReminderUpdater().UpdateToDoReminder(toDoReminder);
            ToDo toDo = new ToDoSelector().GetToDoByLoggedInUserName(toDoReminder.ToDoID);
            return Index(toDo);
        }

        [HttpPost]
        public ActionResult Delete(int toDoReminderID)
        {
            ToDo toDoThatReminderBelongsTo = GetToDoByLoggedInUserName(toDoReminderID);
            new ToDoReminderDeletor().DeleteToDoReminder(toDoReminderID, toDoThatReminderBelongsTo);
            return Index(toDoThatReminderBelongsTo);
        }

        private ToDo GetToDoByLoggedInUserName(int toDoReminderID)
        {
            return new ToDoSelector().GetToDoByLoggedInUserName(new ToDoReminderSelector().GetToDoReminder(toDoReminderID).ToDoID);
        }
    }
}