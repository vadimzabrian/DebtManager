using DebtManager.Domain.Debts.Queries;
using DebtManager.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace DebtManager.Domain.DebtCalculations
{
    public class DebtCalculatorForMultiplePeople : IDebtCalculatorForMultiplePeople
    {
        IDebtCalculatorForTwoPeople _debtCalculatorForTwoPeople;
        IExistsDebtForUsers_Query _existsDebtForUsers_Query;

        public DebtCalculatorForMultiplePeople(IDebtCalculatorForTwoPeople debtCalculatorForTwoPeople, IExistsDebtForUsers_Query existsDebtForUsers_Query)
        {
            _debtCalculatorForTwoPeople = debtCalculatorForTwoPeople;
            _existsDebtForUsers_Query = existsDebtForUsers_Query;
        }

        public List<Debt> Execute(IQueryable<Payment> payments)
        {
            var userIds = payments.Select(p => p.Payer.Id).Union(payments.Select(p => p.Receiver.Id)).Distinct().ToList();

            var debts = new List<Debt>();

            foreach (int u1Id in userIds)
            {
                foreach (int u2Id in userIds)
                {
                    if (_existsDebtForUsers_Query.Execute(debts, u1Id, u2Id) || u1Id == u2Id) continue;

                    debts.Add(_debtCalculatorForTwoPeople.Execute(u1Id, u2Id, payments));
                }
            }

            debts.RemoveAll(ad => ad.Amount == 0);
            debts = debts.OrderBy(ad => ad.MustPayId).ThenBy(ad => ad.MustReceiveId).ToList();

            return debts;
        }
    }

    public interface IDebtCalculatorForMultiplePeople
    {
        List<Debt> Execute(IQueryable<Payment> payments);
    }
}
