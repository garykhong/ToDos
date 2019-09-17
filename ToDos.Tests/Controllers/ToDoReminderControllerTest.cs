using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using ToDos.Controllers;
using ToDos.Models;

namespace ToDos.Tests.Controllers
{
    [TestClass]
    public class ToDoReminderControllerTest
    {
        private FakeToDoDBContext fakeToDoDBContext = new FakeToDoDBContext();
        ToDoReminderController controller;
        DateTime todaysDate = new DateTime();

        [TestInitialize]
        public void SetupDependencies()
        {
            controller = new ToDoReminderController();
            fakeToDoDBContext = new FakeToDoDBContextSelector().GetFakeToDoDBContextAfterSettingToDoDBContext();
        }

        [TestMethod]
        public void MoveDueToDosToLastPriorityWithEmptyString_DoesNothing()
        {
            ToDo firstToDo = fakeToDoDBContext.ToDos.First();
            controller.MoveDueToDosToLastPriority(string.Empty, todaysDate);
            Assert.AreEqual(1, firstToDo.OrderID);
        }

        [TestMethod]
        public void MoveDueToDosToLastPriorityWithNull_DoesNothing()
        {
            ToDo firstToDo = fakeToDoDBContext.ToDos.First();
            controller.MoveDueToDosToLastPriority(null, todaysDate);
            Assert.AreEqual(1, firstToDo.OrderID);
        }

        [TestMethod]
        public void DueDateForFirstToDo_MovesFirstToDoToLast()
        {
            ToDo firstToDo = fakeToDoDBContext.ToDos.First();
            todaysDate = new DateTime(2020, 7, 30);
            controller.MoveDueToDosToLastPriority(firstToDo.UserName, todaysDate);
            Assert.AreEqual(5, firstToDo.OrderID);
        }

        [TestMethod]
        public void DueDateForNoToDo_DoesNotMoveFirstToDoTo()
        {
            ToDo firstToDo = fakeToDoDBContext.ToDos.First();
            todaysDate = new DateTime(1999, 6, 25);
            controller.MoveDueToDosToLastPriority(firstToDo.UserName, todaysDate);
            Assert.AreEqual(1, firstToDo.OrderID);
        }
    }
}
