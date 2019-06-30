using System;
using ToDos.Models;

namespace ToDos
{
    public class WebConfigUpdater
    {
        public WebConfigUpdater()
        {
        }

        public void UpdateConnectionStrings()
        {
            System.Configuration.ConfigurationManager.ConnectionStrings[nameof(ToDoDBContext)].ConnectionString = new ConnectionStringSelector().GetToDoDBContextConnectionString();
        }
    }
}