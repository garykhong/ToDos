using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using ToDos.Controllers;

namespace ToDos.Tests.Controllers
{
    [TestClass]
    public class ToDoDeleteControllerTest
    {
        [TestMethod]
        public void EverytimeIndexActionCalled_ViewNameIsIndex()
        {
            ToDoDeleteController toDoDeleteController = new ToDoDeleteController();
            ViewResult viewResult = toDoDeleteController.Index(1);
            Assert.AreEqual("Index", viewResult.ViewName);
        }
    }
}
