using DebtManager.Domain.DebtCalculations;
using System.Collections.Generic;
using System.Linq;

namespace DebtManager.Domain.Debts.Queries
{
    public class ExistsDebtForUsers_Query : IExistsDebtForUsers_Query
    {
        public bool Execute(IEnumerable<Debt> debts, int user1Id, int user2Id)
        {
            return debts.Any(ad => (ad.MustPayId == user1Id && ad.MustReceiveId == user2Id) ||
                (ad.MustPayId == user2Id && ad.MustReceiveId == user1Id));
        }
    }

    public interface IExistsDebtForUsers_Query
    {
        bool Execute(IEnumerable<Debt> debts, int user1Id, int user2Id);
    }
}
