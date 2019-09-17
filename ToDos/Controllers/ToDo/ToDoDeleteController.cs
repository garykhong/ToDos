using System;
using System.Web.Mvc;
using ToDos.Models;
using System.Linq;
using ToDos.Rules;

namespace ToDos.Controllers
{
    public class ToDoDeleteController : Controller
    {
        private UserFinder loggedInUserFinder;

        public ToDoDeleteController()
        {
            this.loggedInUserFinder = new LoggedInUserFinder();
        }

        public ToDoDeleteController(UserFinder loggedInUserFinder)
        {
            this.loggedInUserFinder = loggedInUserFinder;
        }        

        [HttpPost]
        public ActionResult Delete(int toDoID)
        {
            new ToDoDeletor().DeleteToDo(toDoID);
            return RedirectToAction(nameof(ToDoController.Index), nameof(ToDo));
        }
    }
}