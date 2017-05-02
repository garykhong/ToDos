using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDos;
using ToDos.Controllers;
using ToDos.Models;
using System.Data.Entity;
using System.Collections.ObjectModel;

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
            controller = new ToDoController();
            SetFakeToDoDBContext();
        }

        [TestMethod]
        public void Index_ViewNameIsIndex()
        {
            SetToDoIndexResult();
            Assert.AreEqual("Index", toDoIndexResult.ViewName);
        }

        [TestMethod]
        public void Index_WithNoFilterAllToDosAreBoundToModel()
        {
            SetToDoIndexResult();
            Assert.AreEqual(fakeToDoDBContext.FakeToDos.Local.Count(),
                ((IDbSet<ToDo>)toDoIndexResult.Model).Count());
        }

        private void SetToDoIndexResult()
        {
            toDoIndexResult = controller.Index() as ViewResult;
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
            SetToDoControllerDetailsResult();
            AssertToDoDetailsResult();
        }

        private void SetFakeToDoDBContext()
        {
            fakeToDoDBContext.ToDos.Add(new ToDo { ID = 1, WhatToDo = "Buy Groceries" });
            fakeToDoDBContext.ToDos.Add(new ToDo { ID = 2, WhatToDo = "Cook Rice" });
            fakeToDoDBContext.SaveChanges();
            ToDoDBContextFactory.SetToDoDBContext(fakeToDoDBContext);
        }

        private void SetToDoControllerDetailsResult()
        {
            toDoDetailsResult = controller.Details(toDoID) as ViewResult;
        }

        private void AssertToDoDetailsResult()
        {
            ToDo toDo = (ToDo)toDoDetailsResult.ViewData.Model;
            Assert.AreEqual(expectedWhatToDo, toDo.WhatToDo);
            Assert.AreEqual("Details", toDoDetailsResult.ViewName);
        }

        [TestMethod]
        public void Create_ViewNameIsCreate()
        {
            ViewResult toDoCreateResult = controller.Create() as ViewResult;
            Assert.AreEqual(toDoCreateResult.ViewName, "Create");
            Assert.AreEqual(null, toDoCreateResult.Model);           
        }

        [TestMethod]
        public void Create_OneToDoIsCreatedModelToDosIncreaseByOne()
        {
            toDoID = 3;
            SetToDo();
            SetToDoCreateResult();
            RedirectToRouteResult routeResult = toDoCreateResult as RedirectToRouteResult;            
            Assert.AreEqual(3, fakeToDoDBContext.ToDos.Count());
            Assert.AreEqual(routeResult.RouteValues["action"], "Index");
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
            toDoCreateResult = controller.Create(toDo) as ActionResult;
        }

        [TestMethod]
        public void Edit_ViewNameIsEdit()
        {
            controller = new ToDoController();
            SetToDo();
            toDo.WhatToDo = "Go to the gym";
            ViewResult result = controller.Edit(toDo);
            Assert.AreEqual("Edit", result.ViewName);
        }
    }
}
