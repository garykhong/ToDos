namespace ToDos.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using ToDos.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<ToDos.Models.ToDoDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "ToDos.Models.ToDoDBContext";
        }

        protected override void Seed(ToDos.Models.ToDoDBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.ToDos.AddOrUpdate(
                toDo => toDo.ID,
                new ToDo { WhatToDo = "To Do id needs to be incremented" },
                new ToDo { WhatToDo = "Edit and delete functionality needs to be added for To Do"},
                new ToDo { WhatToDo = "format pages for better eye flow"},
                new ToDo { WhatToDo = "decide what to do with header links home, about, contact, register and login"},
                new ToDo { WhatToDo = "Add property when to do was done"},
                new ToDo { WhatToDo = "Format WhatToDo to What to do"},
                new ToDo { WhatToDo = "add filter functionality on Index page"},
                new ToDo { WhatToDo = "Change Index page to All To Dos"}
                );
        }
    }
}
