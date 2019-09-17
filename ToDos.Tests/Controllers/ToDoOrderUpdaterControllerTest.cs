using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using ToDos.Controllers;
using ToDos.Models;
using ToDos.Rules;

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

        [TestMethod]
        public void MovingFirstToDoToLast_HasUpdatedOrderIDToMaxPlusOne()
        {
            ToDo firstToDo = fakeToDoDBContext.ToDos.First();
            controller.MoveToDoDownToLastInPriority(firstToDo.ID, firstToDo.UserName);
            Assert.AreEqual(5, firstToDo.OrderID);
        }

        [TestMethod]
        public void MovingNullToDoToLast_HasNoEffect()
        {
            ToDo nullToDo = new ToDo { ID = 0, UserName = null, OrderID = 0 };
            controller.MoveToDoDownToLastInPriority(nullToDo.ID, nullToDo.UserName);
            Assert.AreEqual(0, nullToDo.OrderID);
        }

        private void SetFakeToDoDBContext()
        {
            fakeToDoDBContext = new FakeToDoDBContextSelector().GetFakeToDoDBContextAfterSettingToDoDBContext();
        }

    }
}
