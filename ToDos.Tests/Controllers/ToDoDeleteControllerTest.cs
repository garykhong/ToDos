using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using ToDos.Controllers;
using ToDos.Models;

namespace ToDos.Tests.Controllers
{
    [TestClass]
    public class ToDoDeleteControllerTest
    {
        private FakeToDoDBContext fakeToDoDBContext = new FakeToDoDBContext();
        private ViewResult toDoDeleteIndexResult;
        private int toDoID;        
        private string expectedWhatToDo;
        private ToDoDeleteController controller;

        [TestMethod]
        public void EverytimeIndexActionCalled_ViewNameIsIndex()
        {
            ToDoDeleteController toDoDeleteController = new ToDoDeleteController();
            ViewResult viewResult = toDoDeleteController.Index((int?)1);
            Assert.AreEqual("Index", viewResult.ViewName);
        }

        [TestMethod]
        public void FirstToDo_HasExpectedWhatToDo()
        {
            SetFakeToDoDBContext();
            controller = new ToDoDeleteController();
            toDoID = 1;
            expectedWhatToDo = "Buy Groceries";
            TestToDoDeleteControllerIndexResult();
        }

        [TestMethod]
        public void RequestToDeleteToDo_IsDeletedFromTheToDoDBContext()
        {
            SetFakeToDoDBContext();
            controller = new ToDoDeleteController();
            toDoID = 1;
            RedirectToRouteResult result = controller.Delete(toDoID) as RedirectToRouteResult;
            Assert.AreEqual(null, fakeToDoDBContext.ToDos.Find(toDoID));
            Assert.AreEqual("ToDo", result.RouteValues["controller"]);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        private void TestToDoDeleteControllerIndexResult()
        {
            SetToDoDeleteControllerIndexResult();
            AssertToDoDetailsResult();
        }

        private void SetFakeToDoDBContext()
        {
            fakeToDoDBContext.ToDos.Add(new ToDo { ID = 1, WhatToDo = "Buy Groceries" });
            fakeToDoDBContext.ToDos.Add(new ToDo { ID = 2, WhatToDo = "Cook Rice" });
            ToDoDBContextFactory.SetToDoDBContext(fakeToDoDBContext);
        }

        private void SetToDoDeleteControllerIndexResult()
        {
            toDoDeleteIndexResult = controller.Index((int?)toDoID) as ViewResult;
        }

        private void AssertToDoDetailsResult()
        {
            ToDo toDo = (ToDo)toDoDeleteIndexResult.ViewData.Model;
            Assert.AreEqual(expectedWhatToDo, toDo.WhatToDo);
            Assert.AreEqual("Index", toDoDeleteIndexResult.ViewName);
        }
    }
}
