using DebtManager.Application.Payments;
using DebtManager.Application.Users;
using DebtManager.Domain.DebtCalculations;
using DebtManager.Domain.Debts;
using DebtManager.Domain.Minimizers;
using DebtManager.Domain.Payments;
using DebtManager.WebAPI.App_Start;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Cors;

namespace DebtManager.WebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [Authorize]
    public class MinimizedDebtsController : ApiController
    {
        [HttpGet]
        public IEnumerable<DebtDto> Get()
        {
            var claimsPrincipal = User as ClaimsPrincipal;
            var username = claimsPrincipal.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;

            #region Dependencies

            var debtCalculatorForMultiplePeople = DependencyResolver.Resolve<IDebtCalculatorForMultiplePeople>();
            var pairMinimizer = DependencyResolver.Resolve<IPairMinimizer>();
            var debtsToDebtDtosConverter = DependencyResolver.Resolve<IDebtsToDebtDtosConverter>();

            var paymentsProvider = DependencyResolver.Resolve<IPaymentsProvider>();
            var usersProvider = DependencyResolver.Resolve<IUsersProvider>();

            #endregion

            var debtsForAllUsers = debtCalculatorForMultiplePeople.Execute(paymentsProvider.Execute().Where(p => p.Status == (int)PaymentStatus.Active));

            var minimizedDebtsForAllUsers = pairMinimizer.Execute(debtsForAllUsers.ToList());

            var minimizedDebtDtosForAllUsers = debtsToDebtDtosConverter.Execute(minimizedDebtsForAllUsers, usersProvider.Execute());

            var debtsWithValues = minimizedDebtDtosForAllUsers.Where(r => r.Amount != 0).ToArray();

            return debtsWithValues.Where(d => d.MustReceiveUsername == username || d.MustPayUsername == username);
        }
    }
}