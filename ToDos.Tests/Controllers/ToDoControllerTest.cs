using System;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDos.Controllers;
using ToDos.Models;
using System.Data.Entity;
using ToDos.Rules;

namespace ToDos.Tests.Controllers
{
    [TestClass]
    public class ToDoControllerTest
    {
        private FakeToDoDBContext fakeToDoDBContext = new FakeToDoDBContext();
        private ViewResult toDoDetailsResult;
        private int toDoID;
        private ToDo toDo;
        private string expectedWhatToDo;
        private ToDoController controller;
        private ViewResult toDoIndexResult;
        private ActionResult toDoCreateResult;

        [TestInitialize]
        public void SetupDependencies()
        {
            SetToDoController();
            SetFakeToDoDBContext();
        }

        private void SetToDoController()
        {
            controller = new ToDoController(new FakeLoggedInUserFinder());
        }

        [TestMethod]
        public void Index_ViewNameIsIndex()
        {
            SetToDoIndexResult();
            Assert.AreEqual(nameof(ToDoController.Index), toDoIndexResult.ViewName);
        }

        [TestMethod]
        public void Index_WithNoFilterAllToDosAreBoundToModel()
        {
            SetToDoIndexResult();
            Assert.AreEqual(fakeToDoDBContext.ToDos.Local.Count(),
                ((IQueryable<ToDo>)toDoIndexResult.Model).Count());
        }

        [TestMethod]
        public void FilterByWhatToDo_WithFilterOneToDoIsBoundToModel()
        {
            toDoIndexResult = controller.FilterByWhatToDo("rice");
            Assert.AreEqual(2, ((IQueryable<ToDo>)toDoIndexResult.Model).FirstOrDefault().ID);
        }

        [TestMethod]
        public void Index_ToDosAreSortedNewestIncompleteToDo()
        {
            SetToDoIndexResult();
            Assert.AreEqual(2, ((IQueryable<ToDo>)toDoIndexResult.Model).FirstOrDefault().ID);
        }

        [TestMethod]
        public void FilterByWhatToDo_WithNoFilterAllToDosAreBoundToModel()
        {
            toDoIndexResult = controller.FilterByWhatToDo("");
            Assert.AreEqual(fakeToDoDBContext.ToDos.Count(), 
                ((IQueryable<ToDo>)toDoIndexResult.Model).Count());
        }

        private void SetToDoIndexResult()
        {
            toDoIndexResult = controller.Index() as ViewResult;
        }

        private void SetFakeToDoDBContext()
        {
            fakeToDoDBContext = new FakeToDoDBContextSelector().GetFakeToDoDBContextAfterSettingToDoDBContext();
        }

        [TestMethod]
        public void Create_ViewNameIsCreate()
        {
            ViewResult toDoCreateResult = controller.Create() as ViewResult;
            Assert.AreEqual(toDoCreateResult.ViewName, nameof(ToDoController.Create));
            Assert.AreNotEqual(null, toDoCreateResult.Model);           
        }

        [TestMethod]
        public void Create_OneToDoIsCreatedModelToDosIncreaseByOne()
        {
            toDoID = 3;
            int expectedToDoID = fakeToDoDBContext.ToDos.Last().ID + 1;
            SetToDo();
            SetToDoCreateResult();            
            RedirectToRouteResult routeResult = toDoCreateResult as RedirectToRouteResult;            
            Assert.AreEqual(expectedToDoID, fakeToDoDBContext.ToDos.Count());
            Assert.AreEqual(routeResult.RouteValues[ParameterName.actionParameterName], nameof(ToDoController.Index));
        }

        [TestMethod]
        public void Create_NoIDForToDoSpecifiedGetsZero()
        {
            toDoID = 0;
            SetToDo();
            SetToDoCreateResult();
            Assert.AreEqual(0, fakeToDoDBContext.ToDos.Last().ID);
            Assert.AreEqual(DateTime.Today, fakeToDoDBContext.ToDos.Last().WhenItWasDone);
        }

        [TestMethod]
        public void Model_WhenItWasDoneIsNullable()
        {
            SetToDo();
            toDo.WhenItWasDone = null;            
            SetToDoCreateResult();
            Assert.AreEqual(null, fakeToDoDBContext.ToDos.Last().WhenItWasDone);
        }

        private void SetToDo()
        {
            toDo = new ToDo
            {
                ID = toDoID,
                WhatToDo = "Buy CDs",
                WhenItWasDone = DateTime.Today
            };
        }

        private void SetToDoCreateResult()
        {
            toDoCreateResult = controller.Create(toDo, null) as ActionResult;
        }

        [TestMethod]
        public void Edit_ViewNameIsEdit()
        {            
            toDoID = 2;
            SetToDo();            
            ViewResult result = (ViewResult)controller.Edit(toDo.ID);
            Assert.AreEqual(nameof(ToDoController.Edit), result.ViewName);
        }

        [TestMethod]
        public void Edit_RequestToEditToDo_IsFound()
        {                    
            ViewResult result = (ViewResult)controller.Edit(fakeToDoDBContext.ToDos.First().ID);
            Assert.AreEqual(fakeToDoDBContext.ToDos.First().WhatToDo, ((ToDo)result.Model).WhatToDo);
        }

        [TestMethod]
        public void Edit_RequestToSaveEditedToDo_IsUpdatedInTheToDoDBContext()
        {
            controller = new MockToDoController();
            fakeToDoDBContext.ToDos.First().WhatToDo = "Buy candy";
            RedirectToRouteResult result = controller.Edit(fakeToDoDBContext.ToDos.First()) as RedirectToRouteResult;
            
            Assert.AreEqual(nameof(ToDoController.Index), result.RouteValues[ParameterName.actionParameterName]);
            Assert.AreEqual(true, fakeToDoDBContext.SaveChangesWasCalled);
            Assert.AreEqual(EntityState.Modified, fakeToDoDBContext.GetFakeToDoEntryState());
        }

    }
}
