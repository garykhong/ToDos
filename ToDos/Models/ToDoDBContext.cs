using System;
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
            string toDoDBContextConnectionString = GetToDoDBContextConnectionString();
            if (toDoDBContextConnectionString != string.Empty)
                base.Database.Connection.ConnectionString = new ConnectionStringSelector().GetToDoDBContextConnectionString();
        }

        private string GetToDoDBContextConnectionString()
        {
            if (Type.GetType("WebConfigurationManager") == null)
            {
                return string.Empty;
            }
            return new ConnectionStringSelector().GetToDoDBContextConnectionString();
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
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ToDoDBContext, Migrations.Configuration>());            
        }

        private void SetContextConnectionString(ToDoDBContext context)
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