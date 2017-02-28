using System.Web.Mvc;
using System.Web.Routing;

namespace MyWebApplication
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            
            /* 
                * The routes specified here are responsible for mapping the URL you type in the browser's address bar to the controllers and their actions in your C# code..
                * The order you register routes matters A LOT!
                * The routers specified on the top have the highest priority. If you specify two routes that correspond to identical URL, the first one will be picked.
                * Rule of thumb is to specify the most concrete routes first and most abstract ones last. This pattern could be observed in the current file
             */

            routes.MapRoute(
                name: "Cat list",
                url: "Cats",
                defaults: new { controller = "Cats", action = "Index" }
            );

            routes.MapRoute(
                name: "Cat ugly",
                url: "Cats/Cat",
                defaults: new { controller = "Cats", action = "Cat" }
            );

            routes.MapRoute(
                name: "Cat nice",
                url: "Cats/{catId}",
                defaults: new { controller = "Cats", action = "Cat", catId = "" }
            );
             /* This is the default route configuration. It specifies the default behaviour, that was mentioned earlier in the tips.*/
            routes.MapRoute(
                name: "Default", // Name of the route. has to be unique.
                url: "{controller}/{action}/{id}", // Route specification. {controller} represents controller name without "Controller" postfix. {action} represents controller's action. {id} specifies id parameter inside the controller's action. It is not used in oru solution.
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
