using DebtManager.Domain.DebtCalculations;
using DebtManager.Domain.Debts;
using DebtManager.Domain.Dtos;
using DebtManager.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace DebtManager.Domain.Tests.DebtCalculations
{
    [TestClass]
    public class DebtCalculatorForTwoPeople_Tests
    {
        //var adrian = new User { Id = 1, Name = "Adrian" };
        //var bogdan = new User { Id = 2, Name = "Bogdan" };
        //var ovidiu = new User { Id = 3, Name = "Ovidiu" };
        //var vadim = new User { Id = 4, Name = "Vadim" };

        //var payments = new List<Payment>();
        //payments.Add(Payment.FromDto(new PaymentDto { Amount = 17 }, adrian, bogdan));
        //payments.Add(Payment.FromDto(new PaymentDto { Amount = 13 }, adrian, bogdan));
        //payments.Add(Payment.FromDto(new PaymentDto { Amount = 29 }, bogdan, adrian));
        //payments.Add(Payment.FromDto(new PaymentDto { Amount = 119 }, bogdan, adrian));

        //var debts = new List<Debt>();
        //debts.Add(Debt.FromDto(new DebtDto { Amount = 235 }, adrian, bogdan));
        //debts.Add(Debt.FromDto(new DebtDto { Amount = 321 }, adrian, bogdan));
        //debts.Add(Debt.FromDto(new DebtDto { Amount = 53 }, bogdan, adrian));
        //debts.Add(Debt.FromDto(new DebtDto { Amount = 87 }, bogdan, adrian));

        [TestMethod]
        public void Should_Add_Debts()
        {
            var adrian = new User { Id = 1 };
            var bogdan = new User { Id = 2 };

            var payments = new List<Payment>();
            payments.Add(Payment.FromDto(new PaymentDto { Amount = 17 }, adrian, bogdan));
            payments.Add(Payment.FromDto(new PaymentDto { Amount = 13 }, adrian, bogdan));

            var result = new DebtCalculatorForTwoPeople(new DebtNormalizer()).Execute(adrian.Id, bogdan.Id, payments.AsQueryable());

            Assert.AreEqual(30, result.Amount);
            Assert.AreEqual(bogdan.Id, result.MustPayId);
            Assert.AreEqual(adrian.Id, result.MustReceiveId);
        }

        [TestMethod]
        public void Should_Subtract_Debts()
        {
            var adrian = new User { Id = 1 };
            var bogdan = new User { Id = 2 };

            var payments = new List<Payment>();
            payments.Add(Payment.FromDto(new PaymentDto { Amount = 17 }, adrian, bogdan));
            payments.Add(Payment.FromDto(new PaymentDto { Amount = 13 }, bogdan, adrian));

            var result = new DebtCalculatorForTwoPeople(new DebtNormalizer()).Execute(adrian.Id, bogdan.Id, payments.AsQueryable());

            Assert.AreEqual(4, result.Amount);
            Assert.AreEqual(bogdan.Id, result.MustPayId);
            Assert.AreEqual(adrian.Id, result.MustReceiveId);
        }

        [TestMethod]
        public void Should_Handle_NonExisting_Payments()
        {
            var adrian = new User { Id = 1 };
            var bogdan = new User { Id = 2 };

            var payments = new List<Payment>();
            //payments.Add(Payment.FromDto(new PaymentDto { Amount = 17 }, adrian, bogdan));
            //payments.Add(Payment.FromDto(new PaymentDto { Amount = 13 }, bogdan, adrian));

            var result = new DebtCalculatorForTwoPeople(new DebtNormalizer()).Execute(adrian.Id, bogdan.Id, payments.AsQueryable());

            Assert.AreEqual(0, result.Amount);
        }
    }
}
