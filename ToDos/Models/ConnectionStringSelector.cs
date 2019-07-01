using System.Web.Configuration;

namespace ToDos.Models
{
    public class ConnectionStringSelector
    {
        public string GetToDoDBContextConnectionString()
        {
            if (WebConfigurationManager.ConnectionStrings[nameof(ToDoDBContext)] == null)
                return string.Empty;

            return WebConfigurationManager.ConnectionStrings[nameof(ToDoDBContext)].ToString();
        }
    }
}
