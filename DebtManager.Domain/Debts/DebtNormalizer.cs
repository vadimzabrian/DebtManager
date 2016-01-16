using DebtManager.Domain.DebtCalculations;

namespace DebtManager.Domain.Debts
{
    public class DebtNormalizer : IDebtNormalizer
    {
        public Debt Execute(Debt debt)
        {
            if (debt.Amount < 0)
            {
                int tmpUserId = debt.MustPayId;

                debt.MustPayId = debt.MustReceiveId;
                debt.MustReceiveId = tmpUserId;
                debt.Amount = -debt.Amount;
            }

            return debt;
        }
    }

    public interface IDebtNormalizer
    {
        Debt Execute(Debt debt);
    }
}
