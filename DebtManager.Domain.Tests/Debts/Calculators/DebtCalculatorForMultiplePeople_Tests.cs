using DebtManager.Domain.DebtCalculations;
using DebtManager.Domain.Debts;
using DebtManager.Domain.Debts.Queries;
using DebtManager.Domain.Dtos;
using DebtManager.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace DebtManager.Domain.Tests.Debts.Calculators
{
    [TestClass]
    public class DebtCalculatorForMultiplePeople_Tests
    {
        [TestMethod]
        public void Should_Generate_Debts()
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

            var result = new DebtCalculatorForMultiplePeople(new DebtCalculatorForTwoPeople(new DebtNormalizer()), new ExistsDebtForUsers_Query()).Execute(payments.AsQueryable());

            Assert.AreEqual(5, result.Count);

            Assert.AreEqual(168, result.Single(d => d.MustPayId == adrian.Id && d.MustReceiveId == bogdan.Id).Amount);
            Assert.AreEqual(87, result.Single(d => d.MustPayId == ovidiu.Id && d.MustReceiveId == adrian.Id).Amount);
            Assert.AreEqual(43, result.Single(d => d.MustPayId == adrian.Id && d.MustReceiveId == vadim.Id).Amount);
            Assert.AreEqual(6, result.Single(d => d.MustPayId == vadim.Id && d.MustReceiveId == bogdan.Id).Amount);
            Assert.AreEqual(3, result.Single(d => d.MustPayId == vadim.Id && d.MustReceiveId == ovidiu.Id).Amount);
        }
    }
}
