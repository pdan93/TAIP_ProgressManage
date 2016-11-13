using System.IO;
using System.Web;
using System.Web.Http;

namespace ProgressManage
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            log4net.Config.XmlConfigurator.Configure();
        }
    }
}
