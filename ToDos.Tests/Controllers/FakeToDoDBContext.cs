using ToDos.Models;
using System.Collections.Generic;
using System.Data.Entity;

namespace ToDos.Tests.Controllers
{
    internal class FakeToDoDBContext : ToDoDBContext
    {
        private IDbSet<ToDo> fakeToDos = new FakeDbSet<ToDo>();

        public FakeToDoDBContext()
        {
            base.ToDos = fakeToDos;
        }
        public IDbSet<ToDo> FakeToDos
        {
            get { return fakeToDos; }
            set { }
        }        
    }
}