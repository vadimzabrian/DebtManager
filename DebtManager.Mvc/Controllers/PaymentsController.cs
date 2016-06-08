using DebtManager.Mvc.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Linq;
using System.Configuration;


namespace DebtManager.Mvc.Controllers
{
    [Authorize]
    public class PaymentsController : Controller
    {
        // GET: Payments
        public async Task<ActionResult> Index()
        {
            IEnumerable<PaymentVM> output = new List<PaymentVM>();

            var claimsPrincipal = User as ClaimsPrincipal;

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                    claimsPrincipal.FindFirst("access_token").Value);

                HttpResponseMessage response = await client.GetAsync(ConfigurationManager.AppSettings["DebtManagerApiUrl"] + "/Payments");
                if (response.IsSuccessStatusCode)
                {
                    var payment = await response.Content.ReadAsStringAsync();
                    output = JsonConvert.DeserializeObject<IEnumerable<PaymentVM>>(payment);
                }
            }

            return View(output);
        }

        // Post: Payments
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            IList<UserVM> users = new List<UserVM>();

            var claimsPrincipal = User as ClaimsPrincipal;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", claimsPrincipal.FindFirst("access_token").Value);

                HttpResponseMessage response = await client.GetAsync(ConfigurationManager.AppSettings["DebtManagerApiUrl"] + "/Users");
                if (response.IsSuccessStatusCode)
                {
                    var usersContent = await response.Content.ReadAsStringAsync();
                    users = JsonConvert.DeserializeObject<IList<UserVM>>(usersContent);
                }
            }

            var model = new PaymentVM();
            model.Users = new SelectList(users.Select(u => new SelectListItem { Text = u.Name, Value = u.Id.ToString() }), "Value", "Text");

            return View(model);
        }

        // Post: Payments
        [HttpPost]
        public async Task<ActionResult> Create(PaymentVM payment)
        {

            var claimsPrincipal = User as ClaimsPrincipal;

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                    claimsPrincipal.FindFirst("access_token").Value);

                var content = new FormUrlEncodedContent(new[] 
                    {
                        new KeyValuePair<string, string>("PayerId", payment.PayerId.ToString()),
                        new KeyValuePair<string, string>("ReceiverId", payment.ReceiverId.ToString()),
                        new KeyValuePair<string, string>("Amount", payment.Amount.ToString()),
                        new KeyValuePair<string, string>("Reason", payment.Reason.ToString())
                    });

                // New code:
                HttpResponseMessage response = await client.PostAsync(ConfigurationManager.AppSettings["DebtManagerApiUrl"] + "/Payments", content);
            }

            return RedirectToAction("Index");
        }
    }
}