using System;

namespace ToDos.Models
{
    public class ToDoDBContextFactory
    {
        private static ToDoDBContext ToDoDBContext;

        public static ToDoDBContext Create()
        {
            if(ToDoDBContext == null)
            {
                ToDoDBContext = new Models.ToDoDBContext();
                ToDoDBContext.Configuration.ProxyCreationEnabled = false;
            }
            return ToDoDBContext;
        }

        public static void SetToDoDBContext(ToDoDBContext toDoDBContext)
        {
            ToDoDBContext = toDoDBContext;
        }
    }
}