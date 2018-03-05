using System;
using System.Web.Mvc;
using ToDos.Models;
using System.Linq;
using Microsoft.AspNet.Identity;
using ToDos.Controllers.Attributes;
using System.Collections.Generic;

namespace ToDos.Controllers
{
    [RequireHttpsForRemoteRequest]
    [Authorize]
    public class ToDoController : Controller
    {        
        public ViewResult Index()
        {            
            return View("Index", GetSortedToDosByLoggedInUserName());
        }

        private IOrderedQueryable<ToDo> GetSortedToDosByLoggedInUserName()
        {
            string userName = GetLoggedInUserName();

            return ToDoDBContextFactory.Create().ToDos.
                          Where(toDo => toDo.UserName == userName).
                           OrderBy(toDo => toDo.WhenItWasDone).
                             ThenByDescending(toDo => toDo.ID);
        }

        public ViewResult FilterByWhatToDo(string whatToDoContainsThis)
        {
            if (whatToDoContainsThis == string.Empty)
            {
                return Index();
            }

            var toDosFound = ToDoDBContextFactory.Create().
                ToDos.Where(toDo => toDo.WhatToDo.ToLower().
                               Contains(whatToDoContainsThis.ToLower()));
            return View("Index", toDosFound);
        }

        public ViewResult Details(int? toDoID)
        {
            ToDo toDo = ToDoDBContextFactory.Create().
                                 ToDos.Where(td => td.ID == toDoID).First();
            return View("Details", toDo);
        }

        public ViewResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        public ActionResult Create(ToDo toDo, string MaintainFiles)
        {
            SaveToDoWithLoggedInUserName(toDo);
            if(MaintainFiles != string.Empty)
            {
                return RedirectToAction("Index", "ToDoFile", new { toDoID = toDo.ID});
            }
            return RedirectToAction("Index");
        }

        private void SaveToDoWithLoggedInUserName(ToDo toDo)
        {
            toDo.UserName = GetLoggedInUserName();
            if(toDo.ToDoFiles == null)
            {
                toDo.ToDoFiles = new List<ToDoFile>();
            }
            ToDoDBContextFactory.Create().ToDos.Add(toDo);
            ToDoDBContextFactory.Create().SaveChanges();
        }

        [HttpGet]
        public ViewResult Edit(int? toDoID)
        {
            return View("Edit",
                ToDoDBContextFactory.Create().ToDos.Find(toDoID));
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "ID,WhatToDo,WhenItWasDone,UserName")]ToDo toDo)
        {
            ResetToDoDBContext();
            ToDoDBContextFactory.Create().SetToDoEntryState(toDo);
            ToDoDBContextFactory.Create().SaveChanges();
            return RedirectToAction("Index");
        }

        protected virtual void ResetToDoDBContext()
        {
            ToDoDBContextFactory.SetToDoDBContext(new ToDoDBContext());
        }

        private string GetLoggedInUserName()
        {
            string userName = User == null ? null : User.Identity.GetUserName();
            return userName;
        }
    }
}