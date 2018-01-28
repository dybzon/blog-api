using BlogApi.Configuration;
using System.Web.Http;

namespace BlogApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration.Filters.Add(new CustomHeaderFilter()); // Allow CORS
            GlobalConfiguration.Configuration.Formatters.Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);
        }
    }
}
