using DebtManager.Application;
using DebtManager.Application.Payments;
using DebtManager.Domain.Dtos;
using DebtManager.WebAPI.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace DebtManager.WebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PaymentsController : ApiController
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<PaymentDto> Get()
        {
            return DependencyResolver.Resolve<IPaymentsProvider>().Execute().OrderByDescending(u => u.Date)
                .Select(p => new PaymentDto
                {
                    Id = p.Id,
                    PayerId = p.Payer != null ? p.Payer.Id : 0,
                    PayerName = p.Payer != null ? p.Payer.Name : String.Empty,
                    ReceiverId = p.Receiver != null ? p.Receiver.Id : 0,
                    ReceiverName = p.Receiver != null ? p.Receiver.Name : String.Empty,
                    Amount = p.Amount,
                    Date = p.Date,
                    Reason = p.Reason
                }).ToArray();
        }

        // POST api/values
        [HttpPost]
        public HttpResponseMessage Post(PaymentDto payment)
        {
            var result = DependencyResolver.Resolve<IPaymentCreator>().Execute(payment);

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, result);
            return response;
        }
    }
}
