using System.Web.Http;

namespace BlogApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API routes
            config.MapHttpAttributeRoutes();

            // Enable CORS. Might not be necessary after all?
            config.EnableCors();
        }
    }
}
