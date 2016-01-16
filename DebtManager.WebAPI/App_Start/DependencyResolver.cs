using System.Web.Http;

namespace DebtManager.WebAPI.App_Start
{
    public static class DependencyResolver
    {
        public static T Resolve<T>()
        {
            return (T)GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(T));
        }
    }
}