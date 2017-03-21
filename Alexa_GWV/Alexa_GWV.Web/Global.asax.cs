using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Alexa_GWV.Web
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
            if (ConfigurationManager.AppSettings.Get("RunMigrations").Equals("true",StringComparison.InvariantCultureIgnoreCase))
            {
                var configuration = new Alexa_GWV.Web.Migrations.Configuration();
                var migrator = new DbMigrator(configuration);
                migrator.Update();
            }
           
        }
    }
}
