using System;
using System.Web.Mvc;
using ToDos.Models;
using System.Linq;

namespace ToDos.Controllers
{
    public class ToDoDeleteController : Controller
    {        
        public ToDoDeleteController()
        {
        }

        public ViewResult Index(int? toDoID)
        {
            ToDo toDoToBeDeleted = ToDoDBContextFactory.Create().
                ToDos.Where(toDo => toDo.ID == toDoID).FirstOrDefault();
            return View("Index", toDoToBeDeleted);
        }

        [HttpPost]
        public ActionResult Index(int toDoID)
        {
            ToDo toDoToBeDeleted = ToDoDBContextFactory.Create().
                ToDos.Where(toDo => toDo.ID == toDoID).FirstOrDefault();
            ToDoDBContextFactory.Create().ToDos.Remove(toDoToBeDeleted);
            ToDoDBContextFactory.Create().SaveChanges();
            return RedirectToAction("Index", "ToDo");
        }
    }
}