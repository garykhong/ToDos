using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToDos.Models;
using ToDos.Rules;

namespace ToDos.Controllers
{
    public class ToDoOrderUpdaterController : Controller
    {
        // GET: ToDoOrderUpdater
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult MoveToDoUpInPriorityByToDo(ToDo toDo)
        {
            return MoveToDoUpInPriority(toDo.ID, toDo.UserName);
        }


        [HttpPost]
        public ActionResult MoveToDoUpInPriority(int toDoID, string userName)
        {
            ToDoSelector toDoSelector = new ToDoSelector();
            ToDo toDo = toDoSelector.GetToDo(toDoID, userName);
            ToDo nextToDoThatIsHigherInPriority = toDoSelector.
                GetNextToDoThatIsHigherInPriority(toDo.OrderID, toDo.UserName);
            SwapToDosOrderID(toDo, nextToDoThatIsHigherInPriority);
            return RedirectToAction(nameof(Index), "ToDo");
        }

        private void SwapToDosOrderID(ToDo toDoWithLowerOrderID, ToDo toDoWithHigherOrderID)
        {
            if(toDoWithLowerOrderID.OrderID > 0 && toDoWithHigherOrderID.OrderID > 0)
            {
                int lowerOrderID = toDoWithLowerOrderID.OrderID;
                toDoWithLowerOrderID.OrderID = toDoWithHigherOrderID.OrderID;
                toDoWithHigherOrderID.OrderID = lowerOrderID;
                ToDoUpdater toDoUpdater = new ToDoUpdater();
                toDoUpdater.UpdateToDo(toDoWithLowerOrderID);
                toDoUpdater.UpdateToDo(toDoWithHigherOrderID);
            }
        }

        public void MoveToDoDownInPriority(ToDo toDo)
        {
            ToDo nextToDoWithLowerOrderID = new ToDoSelector().
                GetNextToDoThatIsLowerInPriority(toDo.OrderID, toDo.UserName);
            SwapToDosOrderID(nextToDoWithLowerOrderID, toDo);
        }
    }
}