using Microsoft.Owin;
using Owin;
using System.Data.Entity;
using System.Web.Services.Description;
using ToDos.Models;

[assembly: OwinStartupAttribute(typeof(ToDos.Startup))]
namespace ToDos
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);            
        }
    }
}
