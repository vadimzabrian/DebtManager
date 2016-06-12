using DebtManager.Domain.Dtos;
using DebtManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DebtManager.Application.Payments.Queries
{
    public class PaymentsForUser_Query
    {
        IPaymentsProvider _paymentsProvider;

        public PaymentsForUser_Query(IPaymentsProvider paymentsProvider)
        {
            _paymentsProvider = paymentsProvider;
        }

        public IList<PaymentDto> ExecuteFor(string username)
        {
            var payments = _paymentsProvider.Execute()
                .OrderByDescending(u => u.Date)
                .Where(p => p.Payer.Username == username || p.Receiver.Username == username)
                .Select(p => new PaymentDto
                {
                    Id = p.Id,
                    PayerId = p.Payer != null ? p.Payer.Id : 0,
                    PayerName = p.Payer != null ? p.Payer.Name : String.Empty,
                    PayerUsername = p.Payer != null ? p.Payer.Username : String.Empty,
                    ReceiverId = p.Receiver != null ? p.Receiver.Id : 0,
                    ReceiverName = p.Receiver != null ? p.Receiver.Name : String.Empty,
                    ReceiverUsername = p.Receiver != null ? p.Receiver.Username : String.Empty,
                    Amount = p.Amount,
                    Date = p.Date,
                    Reason = p.Reason,
                    Status = p.Status
                }).ToList();

            foreach (var p in payments)
            {
                p.StatusName = Payment.GetStatusNameFor(p.Status);
            }

            return payments.ToArray();
        }
    }
}