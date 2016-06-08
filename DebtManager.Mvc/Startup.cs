using Microsoft.AspNet.Identity;
using Microsoft.IdentityModel.Protocols;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Security.Claims;
using System.Threading.Tasks;

[assembly: OwinStartupAttribute(typeof(DebtManager.Mvc.Startup))]
namespace DebtManager.Mvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            JwtSecurityTokenHandler.InboundClaimTypeMap = new Dictionary<string, string>();

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie
            });

            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
            {
                ClientId = "socialnetwork_implicit",
                Authority = "http://localhost:24837/",
                RedirectUri = "http://localhost:22250/",
                ResponseType = "token id_token",
                Scope = "openid profile",
                PostLogoutRedirectUri = "http://localhost:22250/",
                SignInAsAuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                Notifications = new OpenIdConnectAuthenticationNotifications
                {
                    SecurityTokenValidated = notification =>
                        {
                            var identity = notification.AuthenticationTicket.Identity;

                            identity.AddClaim(new Claim("id_token", notification.ProtocolMessage.IdToken));
                            identity.AddClaim(new Claim("access_token", notification.ProtocolMessage.AccessToken));

                            notification.AuthenticationTicket = new AuthenticationTicket(identity, notification.AuthenticationTicket.Properties);

                            return Task.FromResult(0);
                        },

                    RedirectToIdentityProvider = notification =>
                        {
                            if (notification.ProtocolMessage.RequestType != OpenIdConnectRequestType.LogoutRequest)
                            {
                                return Task.FromResult(0);
                            }

                            notification.ProtocolMessage.IdTokenHint =
                                notification.OwinContext.Authentication.User.FindFirst("id_token").Value;

                            return Task.FromResult(0);
                        }
                }

            });

            //ConfigureAuth(app);
        }
    }
}
