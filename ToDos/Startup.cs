using Microsoft.Owin;
using Owin;

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
