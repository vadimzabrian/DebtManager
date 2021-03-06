﻿using DebtManager.Application;
using DebtManager.Application.Payments;
using DebtManager.Application.Payments.Queries;
using DebtManager.Domain.Dtos;
using DebtManager.WebAPI.App_Start;
using DebtManager.WebAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Cors;

namespace DebtManager.WebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [Authorize]
    public class PaymentsController : ApiController
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<PaymentDto> Get()
        {
            var claimsPrincipal = User as ClaimsPrincipal;
            var username = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier).Value;

            return DependencyResolver.Resolve<PaymentsForUser_Query>().ExecuteFor(username).ToArray();
        }


        // POST api/values
        [HttpPost]
        public HttpResponseMessage Post(PaymentDto payment)
        {
            var claimsPrincipal = User as ClaimsPrincipal;
            var username = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier).Value;
            payment.PayerUsername = username;

            var result = DependencyResolver.Resolve<IPaymentCreator>().Execute(payment);

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, result);
            return response;
        }


        // PUT api/values
        [HttpPut]
        public HttpResponseMessage Put(PaymentPutModel model)
        {
            var claimsPrincipal = User as ClaimsPrincipal;
            var username = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (model.Action == "confirm")
            {
                DependencyResolver.Resolve<PaymentUpdater>().Confirm(model.Id, username);
            }

            if (model.Action == "reject")
            {
                DependencyResolver.Resolve<PaymentUpdater>().Reject(model.Id, username);
            }

            if (model.Action == "delete")
            {
                DependencyResolver.Resolve<PaymentUpdater>().Delete(model.Id, username);
            }

            if (model.Action == "resend")
            {
                DependencyResolver.Resolve<PaymentUpdater>().Resend(model.Id, username);
            }

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }
    }
}
