using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using ToDos.Controllers;
using ToDos.Models;
using ToDos.Rules;

namespace ToDos.Tests.Controllers
{
    [TestClass]
    public class ToDoDeleteControllerTest
    {
        private FakeToDoDBContext fakeToDoDBContext = new FakeToDoDBContext();
        private int toDoID;        
        private ToDoDeleteController controller;  

        [TestMethod]
        public void RequestToDeleteToDo_IsDeletedFromTheToDoDBContext()
        {
            SetFakeToDoDBContext();
            controller = new ToDoDeleteController();
            toDoID = 1;
            RedirectToRouteResult result = controller.Delete(toDoID) as RedirectToRouteResult;
            Assert.AreEqual(null, fakeToDoDBContext.ToDos.Find(toDoID));
            Assert.AreEqual(nameof(ToDo), result.RouteValues[ParameterName.controllerParameterName]);
            Assert.AreEqual(nameof(ToDoController.Index), result.RouteValues[ParameterName.actionParameterName]);
        }
       
        private void SetFakeToDoDBContext()
        {
            fakeToDoDBContext = new FakeToDoDBContextSelector().GetFakeToDoDBContextAfterSettingToDoDBContext();
        }               
    }
}
