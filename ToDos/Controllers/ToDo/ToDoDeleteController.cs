using System;
using System.Web.Mvc;
using ToDos.Models;
using System.Linq;
using ToDos.Rules;

namespace ToDos.Controllers
{
    public class ToDoDeleteController : Controller
    {        
        public ToDoDeleteController()
        {
        }

        public ViewResult Index(int? toDoID)
        {            
            ToDo toDoToBeDeleted = new ToDoSelector().GetToDoByLoggedInUserName(toDoID);
            return View("Index", toDoToBeDeleted);
        }

        [HttpPost]
        public ActionResult Delete(int toDoID)
        {
            new ToDoDeletor().DeleteToDo(toDoID);
            return RedirectToAction("Index", "ToDo");
        }
    }
}