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
            controller.MoveToDoUpInPriorityByToDo(firstToDo);
            Assert.AreEqual(1, firstToDo.OrderID);
        }

        [TestMethod]
        public void MovingFirstToDoDownInPriority_HasSwappedOrderID()
        {
            ToDo firstToDo = fakeToDoDBContext.ToDos.FirstOrDefault();
            ToDo secondToDo = fakeToDoDBContext.ToDos.Where(td => td.OrderID == 2).FirstOrDefault();
            controller.MoveToDoDownInPriorityByToDo(firstToDo);
            Assert.AreEqual(2, firstToDo.OrderID);
            Assert.AreEqual(1, secondToDo.OrderID);
        }

        [TestMethod]
        public void MovingLastDoneToDoUpInPriority_HasNoEffect()
        {
            ToDo lastToDo = fakeToDoDBContext.ToDos.Last();
            int lastToDoOrderID = lastToDo.OrderID;
            ToDo secondToDo = fakeToDoDBContext.ToDos.Where(td => td.OrderID == 2).FirstOrDefault();
            controller.MoveToDoUpInPriorityByToDo(lastToDo);            
            Assert.AreEqual(lastToDoOrderID, lastToDo.OrderID);
        }

        [TestMethod]
        public void MovingLastToDoDownInPriority_HasNoEffectOnOrderID()
        {
            ToDo lastToDo = fakeToDoDBContext.ToDos.Last();
            int lastToDoOrderID = lastToDo.OrderID;
            controller.MoveToDoDownInPriorityByToDo(lastToDo);
            Assert.AreEqual(lastToDoOrderID, lastToDo.OrderID);            
        }

        [TestMethod]
        public void MovingDoneToDoDownInPriority_HasNoEffectOnOrderID()
        {
            ToDo doneToDo = fakeToDoDBContext.ToDos.Where(td => td.OrderID == 3).FirstOrDefault();
            int doneToDoOrderID = doneToDo.OrderID;
            controller.MoveToDoDownInPriorityByToDo(doneToDo);
            Assert.AreEqual(doneToDoOrderID, doneToDo.OrderID);
        }

        [TestMethod]
        public void MovingDoneToDoUpInPriority_HasNoEffectOnOrderID()
        {
            ToDo doneToDo = fakeToDoDBContext.ToDos.Where(td => td.OrderID == 3).FirstOrDefault();
            int doneToDoOrderID = doneToDo.OrderID;
            controller.MoveToDoUpInPriorityByToDo(doneToDo);
            Assert.AreEqual(doneToDoOrderID, doneToDo.OrderID);
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
            fakeToDoDBContext.ToDos.Add(new ToDo
            {
                ID = 4,
                WhatToDo = "Clean room",
                WhenItWasDone = new DateTime(2013, 10, 12),
                OrderID = 4
            });
            ToDoDBContextFactory.SetToDoDBContext(fakeToDoDBContext);
        }

    }
}
