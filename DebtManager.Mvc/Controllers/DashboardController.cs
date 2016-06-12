using DebtManager.Mvc.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace DebtManager.Mvc.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        // GET: Dashboard data
        public async Task<ActionResult> Index()
        {
            IEnumerable<DebtVM> debts = new List<DebtVM>();
            BalanceVM balance = null;

            var claimsPrincipal = User as ClaimsPrincipal;

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", claimsPrincipal.FindFirst("access_token").Value);

                HttpResponseMessage response = await client.GetAsync(ConfigurationManager.AppSettings["DebtManagerApiUrl"] + "/MinimizedDebts");
                if (response.IsSuccessStatusCode)
                {
                    var debtsData = await response.Content.ReadAsStringAsync();
                    debts = JsonConvert.DeserializeObject<IEnumerable<DebtVM>>(debtsData);
                }
            }

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", claimsPrincipal.FindFirst("access_token").Value);


                HttpResponseMessage responseBalance = await client.GetAsync(ConfigurationManager.AppSettings["DebtManagerApiUrl"] + "/Balance");
                if (responseBalance.IsSuccessStatusCode)
                {
                    var balanceString = await responseBalance.Content.ReadAsStringAsync();
                    balance = JsonConvert.DeserializeObject<BalanceVM>(balanceString);
                }
            }

            return View(new DashboardVM { Debts = debts, Balance = balance, LoggedInUsername = claimsPrincipal.FindFirst("sub").Value });
        }
    }
}