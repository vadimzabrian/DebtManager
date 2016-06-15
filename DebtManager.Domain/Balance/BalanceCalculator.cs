using DebtManager.Domain.DebtCalculations;
using DebtManager.Domain.Entities;
using DebtManager.Domain.Payments;
using System.Linq;

namespace DebtManager.Domain
{
    public class BalanceCalculator : IBalanceCalculator
    {
        IDebtCalculatorForOnePerson _debtCalculatorForOnePerson;

        public BalanceCalculator(IDebtCalculatorForOnePerson debtCalculatorForOnePerson)
        {
            _debtCalculatorForOnePerson = debtCalculatorForOnePerson;
        }

        public Balance ExecuteFor(int userId, IQueryable<Payment> payments)
        {
            payments = payments.Where(p => p.Status == (int)PaymentStatus.Confirmed);

            var debts = _debtCalculatorForOnePerson.Execute(userId, payments);

            var amountUserMustPay = debts.Where(d => d.MustPayId == userId).Sum(d => d.Amount);
            var amountUserMustReceive = debts.Where(d => d.MustReceiveId == userId).Sum(d => d.Amount);

            var balance = new Balance { UserId = userId };
            if (amountUserMustPay > amountUserMustReceive)
            {
                balance.MustPayAmount = amountUserMustPay - amountUserMustReceive;
            }
            if (amountUserMustPay < amountUserMustReceive)
            {
                balance.MustReceiveAmount = amountUserMustReceive - amountUserMustPay;
            }

            return balance;
        }
    }

    public interface IBalanceCalculator
    {
        Balance ExecuteFor(int userId, IQueryable<Payment> payments);
    }
}
