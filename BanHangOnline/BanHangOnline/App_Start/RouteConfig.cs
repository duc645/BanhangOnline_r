using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BanHangOnline
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "News",
            //    url: "tin-tuc",
            //    defaults: new { controller = "News", action = "Index", id = UrlParameter.Optional },
            //    namespaces: new[] { "BanHangOnline.Controllers" }
            //    );

            //routes.MapRoute(
            //    name: "Contact",
            //    url: "lien-he",
            //    defaults: new { controller = "Contact", action = "Index", id = UrlParameter.Optional },
            //    namespaces: new[] { "BanHangOnline.Controllers" }
            //    );

            //routes.MapRoute(
            //    name: "ProductCategory",
            //    url: "sach",
            //    defaults: new { controller = "Product", action = "Index", id = UrlParameter.Optional },
            //    namespaces: new[] { "BanHangOnline.Controllers" }
            //);

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces : new[] {"BanHangOnline.Controllers"}
            );
        }
    }
}
