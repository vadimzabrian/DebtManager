﻿using DebtManager.Domain.Debts;
using DebtManager.Domain.Entities;
using System.Linq;

namespace DebtManager.Domain.DebtCalculations
{
    public class DebtCalculatorForTwoPeople : IDebtCalculatorForTwoPeople
    {
        IDebtNormalizer _debtNormalizer;

        public DebtCalculatorForTwoPeople(IDebtNormalizer debtNormalizer)
        {
            _debtNormalizer = debtNormalizer;
        }

        public Debt Execute(int user1Id, int user2Id, IQueryable<Payment> payments)
        {
            var debt = new Debt();

            debt.MustPayId = user1Id;
            debt.MustReceiveId = user2Id;

            debt.Amount = 0;

            if (user1Id == user2Id) return debt;

            if (payments != null)
            {
                debt.Amount -= payments.Where(p => p.Payer.Id == user1Id && p.Receiver.Id == user2Id).Sum(p => p.Amount);
                debt.Amount += payments.Where(p => p.Payer.Id == user2Id && p.Receiver.Id == user1Id).Sum(p => p.Amount);
            }

            debt = _debtNormalizer.Execute(debt);

            return debt;
        }
    }

    public interface IDebtCalculatorForTwoPeople
    {
        Debt Execute(int user1Id, int user2Id, IQueryable<Payment> payments);
    }
}
