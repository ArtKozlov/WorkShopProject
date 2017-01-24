using System.Web.Http;
using todoclient.Services;

namespace todoclient
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

           // ProxyService proxyService = ProxyService.GetInstance();
           // proxyService.StartProxy();
        }
    }
}
