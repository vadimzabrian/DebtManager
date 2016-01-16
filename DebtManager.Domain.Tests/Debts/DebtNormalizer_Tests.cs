using DebtManager.Domain.DebtCalculations;
using DebtManager.Domain.Debts;
using DebtManager.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DebtManager.Domain.Tests.Debts
{
    [TestClass]
    public class DebtNormalizer_Tests
    {
        [TestMethod]
        public void Should_Invert_Debtor_And_Creditor()
        {
            var adrian = new User { Id = 1 };
            var bogdan = new User { Id = 2 };

            var debt = new Debt { Amount = -5, MustPayId = adrian.Id, MustReceiveId = bogdan.Id };

            var result = new DebtNormalizer().Execute(debt);

            Assert.AreEqual(bogdan.Id, result.MustPayId);
            Assert.AreEqual(adrian.Id, result.MustReceiveId);
        }

        [TestMethod]
        public void Should_Leave_Debtor_And_Creditor_Unmodified()
        {
            var adrian = new User { Id = 1 };
            var bogdan = new User { Id = 2 };

            var debt = new Debt { Amount = 15, MustPayId = adrian.Id, MustReceiveId = bogdan.Id };

            var result = new DebtNormalizer().Execute(debt);

            Assert.AreEqual(adrian.Id, result.MustPayId);
            Assert.AreEqual(bogdan.Id, result.MustReceiveId);
        }
    }
}
