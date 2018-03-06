using ToDos.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System;
using System.Data.Entity.Infrastructure;

namespace ToDos.Tests.Controllers
{
    internal class FakeToDoDBContext : ToDoDBContext
    {
        private IDbSet<ToDo> fakeToDos = new FakeToDoDbSet<ToDo>();
        private IDbSet<ToDoFile> fakeToDoFiles = new FakeToDoFileDbSet<ToDoFile>();
        private EntityState fakeToDoEntryState = EntityState.Unchanged;

        public FakeToDoDBContext()
        {
            base.ToDos = fakeToDos;
            base.ToDoFiles = fakeToDoFiles;
            SaveChangesWasCalled = false;
        }

        public override IDbSet<ToDo> ToDos
        {
            get { return fakeToDos; }
            set { }
        }

        public override IDbSet<ToDoFile> ToDoFiles
        {
            get { return fakeToDoFiles; }
            set { }
        }

        public bool SaveChangesWasCalled
        {
            get; set;
        }

        public override int SaveChanges()
        {
            SaveChangesWasCalled = true;
            return 0;
        }

        public override void SetToDoEntryState(ToDo toDo)
        {
            fakeToDoEntryState = EntityState.Modified;
        }

        public EntityState GetFakeToDoEntryState()
        {
            return fakeToDoEntryState;
        }
    }
}