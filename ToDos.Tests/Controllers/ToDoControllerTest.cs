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
            FakeToDoDBContext fakeToDoDBContext = new FakeToDoDBContext();
            fakeToDoDBContext.ToDos.Add(new ToDo { ID = 1, WhatToDo = "Buy Groceries" });
            ToDoDBContextFactory.SetToDoDBContext(fakeToDoDBContext);
            ToDoController controller = new ToDoController();
            ViewResult result = controller.Details(1) as ViewResult;
            ToDo toDo = (ToDo)result.ViewData.Model;
            Assert.AreEqual("Buy Groceries", toDo.WhatToDo);
            Assert.AreEqual("Details", result.ViewName);
        }

        [TestMethod]
        public void Details_SecondToDoHasExpectedWhatToDo()
        {
            ToDoController controller = new ToDoController();
            ViewResult result = controller.Details(2) as ViewResult;
            ToDo toDo = (ToDo)result.ViewData.Model;
            Assert.AreEqual("Cook Rice", toDo.WhatToDo);
            Assert.AreEqual("Details", result.ViewName);
        }
    }
}
