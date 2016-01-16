using DebtManager.Domain.DebtCalculations;
using DebtManager.Domain.Debts;
using DebtManager.Domain.Entities;
using DebtManager.Domain.Minimizers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace DebtManager.Domain.Tests.Debts.Calculators
{
    [TestClass]
    public class PairMinimizer_Tests
    {
        [TestMethod]
        public void Should_Minimize()
        {
            var adrian = new User { Id = 1 };
            var bogdan = new User { Id = 2 };
            var ovidiu = new User { Id = 3 };
            var vadim = new User { Id = 4 };

            var debts = new List<Debt>();
            debts.Add(new Debt { MustPayId = adrian.Id, MustReceiveId = bogdan.Id, Amount = 168 });
            debts.Add(new Debt { MustPayId = adrian.Id, MustReceiveId = vadim.Id, Amount = 43 });
            debts.Add(new Debt { MustPayId = ovidiu.Id, MustReceiveId = adrian.Id, Amount = 87 });
            debts.Add(new Debt { MustPayId = vadim.Id, MustReceiveId = bogdan.Id, Amount = 6 });
            debts.Add(new Debt { MustPayId = vadim.Id, MustReceiveId = ovidiu.Id, Amount = 3 });

            var result = new PairMinimizer(new DebtNormalizer()).Execute(debts);

            Assert.AreEqual(3, result.Count());

            Assert.AreEqual(0, result.Where(d => d.MustReceiveId == adrian.Id).Sum(d => d.Amount));
            Assert.AreEqual(174, result.Where(d => d.MustReceiveId == bogdan.Id).Sum(d => d.Amount));
            Assert.AreEqual(0, result.Where(d => d.MustReceiveId == ovidiu.Id).Sum(d => d.Amount));
            Assert.AreEqual(34, result.Where(d => d.MustReceiveId == vadim.Id).Sum(d => d.Amount));

            Assert.AreEqual(124, result.Where(d => d.MustPayId == adrian.Id).Sum(d => d.Amount));
            Assert.AreEqual(0, result.Where(d => d.MustPayId == bogdan.Id).Sum(d => d.Amount));
            Assert.AreEqual(84, result.Where(d => d.MustPayId == ovidiu.Id).Sum(d => d.Amount));
            Assert.AreEqual(0, result.Where(d => d.MustPayId == vadim.Id).Sum(d => d.Amount));
        }
    }
}
