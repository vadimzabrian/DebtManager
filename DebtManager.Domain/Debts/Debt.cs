using System.Collections.Generic;
using System.Linq;

namespace DebtManager.Domain.DebtCalculations
{
    public class Debt
    {
        public int MustPayId { get; set; }
        public int MustReceiveId { get; set; }
        public int Amount { get; set; }

        public void Normalize()
        {
            if (Amount < 0)
            {
                int tmpUserId = this.MustPayId;

                MustPayId = this.MustReceiveId;
                MustReceiveId = tmpUserId;
                Amount = -this.Amount;
            }
        }

        public static bool ArrayContainsDebtFor(IEnumerable<Debt> aggregatedDebts, int user1Id, int user2Id)
        {
            return aggregatedDebts.Any(ad => (ad.MustPayId == user1Id && ad.MustReceiveId == user2Id) ||
                (ad.MustPayId == user2Id && ad.MustReceiveId == user1Id));
        }
    }
}
