using System;
using System.Web.Mvc;
using ToDos.Models;
using System.Linq;

namespace ToDos.Controllers
{
    public class ToDoController : Controller
    {
        public ViewResult Index()
        {
            return View("Index", ToDoDBContextFactory.Create().ToDos);
        }

        public ViewResult Details(int id)
        {            
            ToDo toDo = ToDoDBContextFactory.Create().
                                 ToDos.Where(td => td.ID == id).First();
            return View("Details", toDo);
        }

        public ViewResult Create()
        {            
            return View("Create");
        }

        [HttpPost]
        public ActionResult Create(ToDo toDo)
        {
            ToDoDBContextFactory.Create().ToDos.Add(toDo);
            ToDoDBContextFactory.Create().SaveChanges();
            return RedirectToAction("Index");
        }

        public ViewResult Edit(ToDo toDo)
        {
            return View("Edit", 
                ToDoDBContextFactory.Create().ToDos.Where(td => td.ID == toDo.ID).FirstOrDefault());
        }
    }
}