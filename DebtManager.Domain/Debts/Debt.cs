using System.Collections.Generic;
using System.Linq;

namespace DebtManager.Domain.DebtCalculations
{
    public class Debt
    {
        public int MustPayId { get; set; }
        public int MustReceiveId { get; set; }
        public int Amount { get; set; }
    }
}
