using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Configuration;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Thinktecture.IdentityModel.Clients;

namespace DebtManager.Mvc.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            try
            {

                var client = new OAuth2Client(new Uri(ConfigurationManager.AppSettings["IdentityServerUrl"] +  "/connect/token"), "socialnetwork", "secret");
                var requestedResponse = client.RequestAccessTokenUserName(username, password, "openid profile offline_access");


                var claims = new[]
            {
                new Claim("access_token", requestedResponse.AccessToken),
                new Claim("refresh_token", requestedResponse.RefreshToken),
                new Claim(ClaimTypes.NameIdentifier, username)
            };

                var claimsIdentity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

                HttpContext.GetOwinContext().Authentication.SignIn(claimsIdentity);

                return RedirectToAction("Index", "Dashboard");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Refresh(string username, string password)
        {
            var claimsPrincipal = User as ClaimsPrincipal;

            var client = new OAuth2Client(new Uri(ConfigurationManager.AppSettings["IdentityServerUrl"] + "/connect/token"), "socialnetwork", "secret");
            var requestedResponse = client.RequestAccessTokenRefreshToken(claimsPrincipal.FindFirst("refresh_token").Value);

            var manager = HttpContext.GetOwinContext().Authentication;

            var refreshIdentity = new ClaimsIdentity(User.Identity);

            refreshIdentity.RemoveClaim(refreshIdentity.FindFirst("access_token"));
            refreshIdentity.RemoveClaim(refreshIdentity.FindFirst("refresh_token"));

            refreshIdentity.AddClaim(new Claim("access_token", requestedResponse.AccessToken));
            refreshIdentity.AddClaim(new Claim("refresh_token", requestedResponse.RefreshToken));

            manager.AuthenticationResponseGrant = new AuthenticationResponseGrant(new ClaimsPrincipal(refreshIdentity), new AuthenticationProperties { IsPersistent = true });

            return RedirectToAction("Index", "Dashboard");
        }
    }
}