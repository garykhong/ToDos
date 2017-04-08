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
            ToDoDBContextFactory.Create().ToDos.Add(new ToDo { ID = 1, WhatToDo = "Buy Groceries" });
            return View("Index", ToDoDBContextFactory.Create().ToDos.Local);
        }

        public ViewResult Details(int id)
        {            
            ToDo toDo = ToDoDBContextFactory.Create().
                                 ToDos.Local.Where(td => td.ID == id).First();
            return View("Details", toDo);
        }
    }
}