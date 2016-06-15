using DebtManager.Domain;
using DebtManager.WebAPI.App_Start;
using System.Security.Claims;
using System.Web.Http;

namespace DebtManager.WebAPI.Controllers
{
    [Authorize]
    public class BalanceController : ApiController
    {
        // GET api/values
        public Balance Get()
        {
            var claimsPrincipal = User as ClaimsPrincipal;
            var username = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier).Value;

            return DependencyResolver.Resolve<DebtManager.Application.IBalanceCalculator>().ExecuteFor(username);
        }
    }
}
