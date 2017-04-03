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
            return View("Index");
        }

        public ViewResult Details(int id)
        {
            ToDoDBContextFactory.Create().ToDos.Add(new ToDo { ID = 1, WhatToDo = "Buy Groceries" });
            Models.ToDo toDo = ToDoDBContextFactory.Create().
                                 ToDos.Local.Where(td => td.ID == id).First();
            return View("Details", toDo);
        }
    }
}