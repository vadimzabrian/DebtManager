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
            IEnumerable<PaymentVM> payments = new List<PaymentVM>();

            var claimsPrincipal = User as ClaimsPrincipal;

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                    claimsPrincipal.FindFirst("access_token").Value);

                HttpResponseMessage response = await client.GetAsync(ConfigurationManager.AppSettings["DebtManagerApiUrl"] + "/Payments");
                if (response.IsSuccessStatusCode)
                {
                    var paymentsString = await response.Content.ReadAsStringAsync();
                    payments = JsonConvert.DeserializeObject<IEnumerable<PaymentVM>>(paymentsString);
                }
            }

            return View(new PaymentsIndexVM { Payments = payments, LoggedInUsername = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier).Value });
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
            model.Users = new SelectList(users.Where(u => u.Username != claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier).Value)
                .Select(u => new SelectListItem { Text = u.Name, Value = u.Id.ToString() }), "Value", "Text");

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

        // Post: Payments
        [HttpGet]
        public async Task<ActionResult> Confirm(int id)
        {
            var claimsPrincipal = User as ClaimsPrincipal;

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                    claimsPrincipal.FindFirst("access_token").Value);

                var content = new FormUrlEncodedContent(new[] 
                    {
                        new KeyValuePair<string, string>("Id", id.ToString()),
                        new KeyValuePair<string, string>("Action", "confirm")
                    });

                // New code:
                HttpResponseMessage response = await client.PutAsync(ConfigurationManager.AppSettings["DebtManagerApiUrl"] + "/Payments", content);
            }

            return RedirectToAction("Index");
        }

        // Post: Payments
        [HttpGet]
        public async Task<ActionResult> Reject(int id)
        {
            var claimsPrincipal = User as ClaimsPrincipal;

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                    claimsPrincipal.FindFirst("access_token").Value);

                var content = new FormUrlEncodedContent(new[] 
                    {
                        new KeyValuePair<string, string>("Id", id.ToString()),
                        new KeyValuePair<string, string>("Action", "reject")
                    });

                // New code:
                HttpResponseMessage response = await client.PutAsync(ConfigurationManager.AppSettings["DebtManagerApiUrl"] + "/Payments", content);
            }

            return RedirectToAction("Index");
        }

        // Post: Payments
        [HttpGet]
        public async Task<ActionResult> Resend(int id)
        {
            var claimsPrincipal = User as ClaimsPrincipal;

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                    claimsPrincipal.FindFirst("access_token").Value);

                var content = new FormUrlEncodedContent(new[] 
                    {
                        new KeyValuePair<string, string>("Id", id.ToString()),
                        new KeyValuePair<string, string>("Action", "resend")
                    });

                // New code:
                HttpResponseMessage response = await client.PutAsync(ConfigurationManager.AppSettings["DebtManagerApiUrl"] + "/Payments", content);
            }

            return RedirectToAction("Index");
        }

        // Post: Payments
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var claimsPrincipal = User as ClaimsPrincipal;

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                    claimsPrincipal.FindFirst("access_token").Value);

                var content = new FormUrlEncodedContent(new[] 
                    {
                        new KeyValuePair<string, string>("Id", id.ToString()),
                        new KeyValuePair<string, string>("Action", "delete")
                    });

                // New code:
                HttpResponseMessage response = await client.PutAsync(ConfigurationManager.AppSettings["DebtManagerApiUrl"] + "/Payments", content);
            }

            return RedirectToAction("Index");
        }
    }
}