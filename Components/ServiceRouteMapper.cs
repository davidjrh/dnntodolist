using DotNetNuke.Web.Api;

namespace DavidRodriguez.Modules.TodoItems.Components
{
    public class ServiceRouteMapper : IServiceRouteMapper
    {
        public void RegisterRoutes(IMapRoute routeManager)
        {
            routeManager.MapHttpRoute("TodoItems",
                                    "default",
                                    "{controller}/{action}",
                                    new[] { "DavidRodriguez.Modules.TodoItems.Components" });
        }

    }
}