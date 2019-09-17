﻿using System;
using System.Configuration;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.Migrations;
using System.Data.Entity.Migrations.Infrastructure;
using System.Linq;

namespace ToDos.Models
{
    public class ToDoDBContext : DbContext
    {
        public virtual IDbSet<ToDo> ToDos { get; set; }
        public virtual IDbSet<ToDoFile> ToDoFiles { get; set; }
        public virtual IDbSet<ToDoReminder> ToDoReminders { get; set; }

        public ToDoDBContext()
        {
            string toDoDBContextConnectionString = new ConnectionStringSelector().GetToDoDBContextConnectionString();
            if(toDoDBContextConnectionString != string.Empty)
                base.Database.Connection.ConnectionString = new ConnectionStringSelector().GetToDoDBContextConnectionString();
        }
       
        public virtual void SetToDoEntryState(ToDo toDo)
        {
            this.Set<ToDo>().AddOrUpdate(toDo);
        }

        public virtual void SetToDoReminderEntryState(ToDoReminder toDoReminder)
        {
            this.Set<ToDoReminder>().AddOrUpdate(toDoReminder);
        }
    }

    public class ToDoContextInitialiser : CreateDatabaseIfNotExists<ToDoDBContext>
    {
        public override void InitializeDatabase(ToDoDBContext context)
        {
            SetContextConnectionString(context);
            base.InitializeDatabase(context);
        }

        private static void SetContextConnectionString(ToDoDBContext context)
        {
            context.Database.Connection.ConnectionString = new ConnectionStringSelector().GetToDoDBContextConnectionString();
        }

        protected override void Seed(ToDoDBContext context)
        {
            SetContextConnectionString(context);
            base.Seed(context);
        }
    }
}