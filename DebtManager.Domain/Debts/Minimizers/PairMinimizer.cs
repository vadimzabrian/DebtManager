using DebtManager.Domain.DebtCalculations;
using System.Collections.Generic;
using DebtManager.Domain.Entities;
using System.Linq;
using DebtManager.Domain.Debts;

namespace DebtManager.Domain.Minimizers
{
    public class PairMinimizer : IPairMinimizer
    {
        IDebtNormalizer _debtNormalizer;

        public PairMinimizer(IDebtNormalizer debtNormalizer)
        {
            _debtNormalizer = debtNormalizer;
        }

        public IQueryable<Debt> Execute(List<Debt> debts)
        {
            var userIds = debts.Select(p => p.MustPayId).Union(debts.Select(p => p.MustReceiveId)).Distinct();

            Debt ad1 = null;
            Debt ad2 = null;

            InitAggregatedDebtPair(ref ad1, ref  ad2, userIds, debts);

            while (ad1 != null && ad2 != null)
            {
                if (ad1.Amount > ad2.Amount)
                {
                    var line = debts.FirstOrDefault(d => (ad1.MustPayId == d.MustPayId && ad2.MustReceiveId == d.MustReceiveId) || (ad1.MustPayId == d.MustReceiveId && ad2.MustReceiveId == d.MustPayId));

                    if (line == null)
                    {
                        line = new Debt { MustPayId = ad1.MustPayId, MustReceiveId = ad2.MustReceiveId, Amount = ad2.Amount };
                        debts.Add(line);
                    }
                    else
                    {
                        if (ad1.MustPayId == line.MustPayId)
                        {
                            line.Amount += ad2.Amount;
                        }
                        else
                        {
                            line.Amount -= ad2.Amount;
                            line = _debtNormalizer.Execute(line);
                        }
                    }

                    ad1.Amount -= ad2.Amount;
                    debts.Remove(ad2);
                }
                // ad1.Amount < ad2.Amount
                else
                {
                    var line = debts.FirstOrDefault(d => (ad1.MustPayId == d.MustPayId && ad2.MustReceiveId == d.MustReceiveId) || (ad1.MustPayId == d.MustReceiveId && ad2.MustReceiveId == d.MustPayId));

                    if (line == null)
                    {
                        line = new Debt { MustPayId = ad1.MustPayId, MustReceiveId = ad2.MustReceiveId, Amount = ad1.Amount };
                        debts.Add(line);
                    }
                    else
                    {
                        if (ad1.MustPayId == line.MustPayId)
                        {
                            line.Amount += ad1.Amount;
                        }
                        else
                        {
                            line.Amount -= ad1.Amount;
                            line = _debtNormalizer.Execute(line);
                        }
                    }

                    ad2.Amount -= ad1.Amount;
                    debts.Remove(ad1);
                }

                ad1 = null;
                ad2 = null;

                InitAggregatedDebtPair(ref ad1, ref ad2, userIds, debts);
            }

            debts = debts.OrderBy(ad => ad.MustPayId).ThenBy(ad => ad.MustReceiveId).ToList();

            return debts.AsQueryable();
        }

        private void InitAggregatedDebtPair(ref Debt ad1, ref Debt ad2, IEnumerable<int> userIds, List<Debt> debts)
        {
            foreach (var uId in userIds)
            {
                ad1 = debts.FirstOrDefault(d => uId == d.MustReceiveId);

                if (ad1 == null) continue;

                ad2 = debts.FirstOrDefault(d => uId == d.MustPayId);

                if (ad2 == null) continue;

                break;
            }
        }
    }

    public interface IPairMinimizer
    {
        IQueryable<Debt> Execute(List<Debt> debts);
    }
}
