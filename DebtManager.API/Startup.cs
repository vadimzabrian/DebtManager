//using IdentityServer3.AccessTokenValidation;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DebtManager.API.Startup))]
namespace DebtManager.API
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //app.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
            //{
            //    Authority = "http://localhost:24837/"
            //});
        }
    }
}
