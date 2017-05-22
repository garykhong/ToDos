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
            ViewResult viewResult = toDoDeleteController.Index(1);
            Assert.AreEqual("Index", viewResult.ViewName);
        }

        [TestMethod]
        public void FirstToDo_HasExpectedWhatToDo()
        {
            SetFakeToDoDBContext();
            controller = new ToDoDeleteController();
            toDoID = 1;
            expectedWhatToDo = "Buy Groceries";
            TestToDoDeleteResult();
        }

        private void TestToDoDeleteResult()
        {
            SetToDoControllerDetailsResult();
            AssertToDoDetailsResult();
        }

        private void SetFakeToDoDBContext()
        {
            fakeToDoDBContext.ToDos.Add(new ToDo { ID = 1, WhatToDo = "Buy Groceries" });
            fakeToDoDBContext.ToDos.Add(new ToDo { ID = 2, WhatToDo = "Cook Rice" });
            ToDoDBContextFactory.SetToDoDBContext(fakeToDoDBContext);
        }

        private void SetToDoControllerDetailsResult()
        {
            toDoDeleteIndexResult = controller.Index(toDoID) as ViewResult;
        }

        private void AssertToDoDetailsResult()
        {
            ToDo toDo = (ToDo)toDoDeleteIndexResult.ViewData.Model;
            Assert.AreEqual(expectedWhatToDo, toDo.WhatToDo);
            Assert.AreEqual("Index", toDoDeleteIndexResult.ViewName);
        }
    }
}
