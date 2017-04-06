using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDos;
using ToDos.Controllers;
using ToDos.Models;

namespace ToDos.Tests.Controllers
{
    [TestClass]
    public class ToDoControllerTest
    {
        private FakeToDoDBContext fakeToDoDBContext = new FakeToDoDBContext();
        private ViewResult toDoDetailsResult;
        private int toDoID;
        private string expectedWhatToDo;

        [TestMethod]
        public void Index_ViewNameIsIndex()
        {
            // Arrange
            ToDoController controller = new ToDoController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.AreEqual("Index", result.ViewName);
        }
        
        [TestMethod]
        public void Details_FirstToDoHasExpectedWhatToDo()
        {
            toDoID = 1;
            expectedWhatToDo = "Buy Groceries";
            TestToDoDetailsResult();
        }

        [TestMethod]
        public void Details_SecondToDoHasExpectedWhatToDo()
        {
            toDoID = 2;
            expectedWhatToDo = "Cook Rice";
            TestToDoDetailsResult();
        }

        private void TestToDoDetailsResult()
        {
            SetFakeToDoDBContext();
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
            ToDoController controller = new ToDoController();
            toDoDetailsResult = controller.Details(toDoID) as ViewResult;
        }

        private void AssertToDoDetailsResult()
        {
            ToDo toDo = (ToDo)toDoDetailsResult.ViewData.Model;
            Assert.AreEqual(expectedWhatToDo, toDo.WhatToDo);
            Assert.AreEqual("Details", toDoDetailsResult.ViewName);
        }
    }
}
