using System.Web.Configuration;

namespace ToDos.Models
{
    public class ConnectionStringSelector
    {
        public string GetToDoDBContextConnectionString()
        {
            string connectionString = WebConfigurationManager.ConnectionStrings[nameof(ToDoDBContext)].ToString();
            return connectionString;
        }
    }
}
