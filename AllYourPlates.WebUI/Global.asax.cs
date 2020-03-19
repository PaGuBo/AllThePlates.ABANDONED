using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using AllYourPlates.WebUI.Infrastructure;

namespace AllYourPlates.WebUI
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("favicon.ico"); //this will have to go

            routes.MapRoute(
                "Default home",
                "",
                new { controller = "Home", action = "Index" }
                );

            routes.MapRoute(
                "Authenitcation",
                "Authenticate",
                new { controller = "Home", action = "Authenticate" }
                );

            //routes.MapRoute(
            //    "Home Action",
            //    "{action}",
            //    new { controller = "Home" }
            //    );

            routes.MapRoute(
                "User Snapshot",
                "{userName}",
                new { controller = "User" , action = "Snapshot" }
                );

            //routes.MapRoute(
            //    null,
            //    "",
            //    new { controller = "User", action = "List", userName = (string)null, page = 1 }
            //    );

            routes.MapRoute(
                null,
                "Page{page}",
                new { controller = "User", action = "List", userName = (string)null },
                new { page = @"\d+" }
            );

            /*
            routes.MapRoute(
                null,
                "{userName}",
                new { controller = "User", action = "List", page = 1 }
            );
            */
            routes.MapRoute(
                null,
                "{controller}/{action}");


        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            string absolutePath = Server.MapPath("~");
            // for some reason, on discountasp.net there's no slash, but on localhost there is
            if (absolutePath.Last() != '\\')
            {
                absolutePath += '\\';
            }
            //string absolutePath1 = Server.MapPath("/");

            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory(absolutePath));
        }
    }
}