using System;
using System.Web.Mvc;

namespace ToDos.Controllers
{
    public class ToDoDeleteController : Controller
    {
        public ToDoDeleteController()
        {
        }

        public ViewResult Index(int? v)
        {
            return View("Index");
        }
    }
}