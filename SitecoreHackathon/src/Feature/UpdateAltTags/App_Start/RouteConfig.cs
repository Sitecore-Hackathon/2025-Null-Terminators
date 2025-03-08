using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SitecoreHackathon.Feature.UpdateAltTags.App_Start
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "UpdateAltText",
                url: "api/UpdateAltText",
                defaults: new { controller = "MediaItem", action = "ProcessMediaItems", id = UrlParameter.Optional },
                namespaces: new[] { "SitecoreHackathon.Feature.UpdateAltTags.Controllers" }
            );
        }
    }
}