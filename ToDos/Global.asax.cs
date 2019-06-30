using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Threading;
using System.Globalization;
using System.Web.Http;
using ToDos.App_Start;
using System.Data.Entity;
using ToDos.Models;

namespace ToDos
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //new DatabaseMigrator().SetDatabaseInitialiser();
            //new DatabaseMigrator().MigrateDatabase();
            //new WebConfigUpdater().UpdateConnectionStrings();            
        }
    }
}
