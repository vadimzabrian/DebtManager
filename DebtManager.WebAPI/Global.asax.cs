using System.Web.Http;
using System.Web.Mvc;

namespace DebtManager.WebAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            UnityConfig.RegisterComponents(); 
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        }
    }
}
