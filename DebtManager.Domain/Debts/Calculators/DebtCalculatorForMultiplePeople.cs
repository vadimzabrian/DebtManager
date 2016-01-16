using DebtManager.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace DebtManager.Domain.DebtCalculations
{
    public class DebtCalculatorForMultiplePeople : IDebtCalculatorForMultiplePeople
    {
        IDebtCalculatorForTwoPeople _debtCalculatorForTwoPeople;

        public DebtCalculatorForMultiplePeople(IDebtCalculatorForTwoPeople debtCalculatorForTwoPeople)
        {
            _debtCalculatorForTwoPeople = debtCalculatorForTwoPeople;
        }

        public IQueryable<Debt> Execute(IQueryable<Payment> payments)
        {
            var userIds = payments.Select(p => p.Payer.Id).Union(payments.Select(p => p.Receiver.Id)).Distinct().ToList();

            var debts = new List<Debt>();

            foreach (int u1Id in userIds)
            {
                foreach (int u2Id in userIds)
                {
                    if (Debt.ArrayContainsDebtFor(debts, u1Id, u2Id) || u1Id == u2Id) continue;

                    debts.Add(_debtCalculatorForTwoPeople.Execute(u1Id, u2Id, payments));
                }
            }

            debts.RemoveAll(ad => ad.Amount == 0);
            debts = debts.OrderBy(ad => ad.MustPayId).ThenBy(ad => ad.MustReceiveId).ToList();

            return debts.AsQueryable();
        }
    }

    public interface IDebtCalculatorForMultiplePeople
    {
        IQueryable<Debt> Execute(IQueryable<Payment> payments);
    }
}
