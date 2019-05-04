using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Web.Mvc;
using ToDos.Controllers;
using ToDos.Models;

namespace ToDos.Tests.Controllers
{
    [TestClass]
    public class ToDoOrderUpdaterControllerTest
    {
        private FakeToDoDBContext fakeToDoDBContext = new FakeToDoDBContext();
        ToDoOrderUpdaterController controller;
        ViewResult toDoOrderUpdaterIndexResult;

        [TestInitialize]
        public void SetupDependencies()
        {
            controller = new ToDoOrderUpdaterController();
            SetFakeToDoDBContext();
        }

        [TestMethod]
        public void MovingFirstToDoUpInPriority_HasNoEffectOnOrderID()
        {
            ToDo firstToDo = fakeToDoDBContext.ToDos.First();
            controller.MoveToDoUpInPriority(firstToDo);
            Assert.AreEqual(1, firstToDo.OrderID);
        }

        [TestMethod]
        public void MovingFirstToDoDownInPriority_HasSwappedOrderID()
        {
            ToDo firstToDo = fakeToDoDBContext.ToDos.FirstOrDefault();
            ToDo secondToDo = fakeToDoDBContext.ToDos.Where(td => td.OrderID == 2).FirstOrDefault();
            controller.MoveToDoDownInPriority(firstToDo);
            Assert.AreEqual(2, firstToDo.OrderID);
            Assert.AreEqual(1, secondToDo.OrderID);
        }

        [TestMethod]
        public void MovingLastToDoUpInPriority_HasSwappedOrderID()
        {
            ToDo lastToDo = fakeToDoDBContext.ToDos.Last();
            ToDo secondToDo = fakeToDoDBContext.ToDos.Where(td => td.OrderID == 2).FirstOrDefault();
            controller.MoveToDoUpInPriority(lastToDo);
            Assert.AreEqual(2, lastToDo.OrderID);
            Assert.AreEqual(3, secondToDo.OrderID);
        }

        [TestMethod]
        public void MovingLastToDoDownInPriority_HasNoEffectOnOrderID()
        {
            ToDo lastToDo = fakeToDoDBContext.ToDos.Last();            
            controller.MoveToDoDownInPriority(lastToDo);
            Assert.AreEqual(3, lastToDo.OrderID);            
        }

        private void SetFakeToDoDBContext()
        {
            fakeToDoDBContext.ToDos.Add(new ToDo { ID = 1, WhatToDo = "Buy Groceries", OrderID = 1 });
            fakeToDoDBContext.ToDos.Add(new ToDo { ID = 2, WhatToDo = "Cook Rice", OrderID = 2 });
            fakeToDoDBContext.ToDos.Add(new ToDo
            {
                ID = 3,
                WhatToDo = "Buy baby groot",
                WhenItWasDone = new DateTime(2012, 10, 12),
                OrderID = 3
            });
            ToDoDBContextFactory.SetToDoDBContext(fakeToDoDBContext);
        }

    }
}
