using DebtManager.Mvc.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
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
            IEnumerable<DebtVM> output = new List<DebtVM>();

            var claimsPrincipal = User as ClaimsPrincipal;

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                    claimsPrincipal.FindFirst("access_token").Value);

                HttpResponseMessage response = await client.GetAsync("http://localhost:58857/api/MinimizedDebts");
                if (response.IsSuccessStatusCode)
                {
                    var debtsData = await response.Content.ReadAsStringAsync();
                    output = JsonConvert.DeserializeObject<IEnumerable<DebtVM>>(debtsData);
                }
            }

            return View(output);
        }
    }
}