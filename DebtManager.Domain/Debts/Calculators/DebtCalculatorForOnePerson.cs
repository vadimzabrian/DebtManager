using DebtManager.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace DebtManager.Domain.DebtCalculations
{
    public class DebtCalculatorForOnePerson : IDebtCalculatorForOnePerson
    {
        IDebtCalculatorForTwoPeople _debtCalculatorForTwoPeople;

        public DebtCalculatorForOnePerson(IDebtCalculatorForTwoPeople debtCalculatorForTwoPeople)
        {
            _debtCalculatorForTwoPeople = debtCalculatorForTwoPeople;
        }

        public IList<Debt> Execute(int userId, IQueryable<Payment> payments)
        {
            var userIds = payments.Select(p => p.Payer.Id).Union(payments.Select(p => p.Receiver.Id)).Except(new[] { userId }).Distinct();

            var debts = new List<Debt>();

            foreach (int uId in userIds)
            {
                debts.Add(_debtCalculatorForTwoPeople.Execute(userId, uId, payments));
            }

            debts.RemoveAll(ad => ad.Amount == 0);
            debts = debts.OrderBy(ad => ad.MustPayId).ThenBy(ad => ad.MustReceiveId).ToList();

            return debts;
        }
    }

    public interface IDebtCalculatorForOnePerson
    {
        IList<Debt> Execute(int userId, IQueryable<Payment> payments);
    }
}
