using DebtManager.Domain.DebtCalculations;
using DebtManager.Domain.Debts;
using DebtManager.Domain.Dtos;
using DebtManager.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace DebtManager.Domain.Tests.Debts.Calculators
{
    [TestClass]
    public class DebtCalculatorForOnePerson_Tests
    {
        [TestMethod]
        public void Should_Calculate_Debts_For_One_Person()
        {
            var adrian = new User { Id = 1 };
            var bogdan = new User { Id = 2 };
            var ovidiu = new User { Id = 3 };
            var vadim = new User { Id = 4 };

            var payments = new List<Payment>();
            payments.Add(Payment.FromDto(new PaymentDto { Amount = 17 }, vadim, bogdan));
            payments.Add(Payment.FromDto(new PaymentDto { Amount = 13 }, ovidiu, vadim));
            payments.Add(Payment.FromDto(new PaymentDto { Amount = 45 }, bogdan, adrian));
            payments.Add(Payment.FromDto(new PaymentDto { Amount = 54 }, ovidiu, vadim));
            payments.Add(Payment.FromDto(new PaymentDto { Amount = 123 }, bogdan, adrian));
            payments.Add(Payment.FromDto(new PaymentDto { Amount = 64 }, vadim, ovidiu));
            payments.Add(Payment.FromDto(new PaymentDto { Amount = 87 }, adrian, ovidiu));
            payments.Add(Payment.FromDto(new PaymentDto { Amount = 23 }, bogdan, vadim));
            payments.Add(Payment.FromDto(new PaymentDto { Amount = 43 }, vadim, adrian));

            var result = new DebtCalculatorForOnePerson(new DebtCalculatorForTwoPeople(new DebtNormalizer())).Execute(ovidiu.Id, payments.AsQueryable());

            var sumToPay = result.Where(d => d.MustPayId == ovidiu.Id).Sum(d => d.Amount);
            var sumToReceive = result.Where(d => d.MustReceiveId == ovidiu.Id).Sum(d => d.Amount);

            Assert.AreEqual(84, sumToPay - sumToReceive);
        }
    }
}
