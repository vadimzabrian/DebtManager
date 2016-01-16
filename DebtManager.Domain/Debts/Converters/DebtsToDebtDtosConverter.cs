using DebtManager.Domain.DebtCalculations;
using DebtManager.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace DebtManager.Domain.Debts
{
    public class DebtsToDebtDtosConverter : IDebtsToDebtDtosConverter
    {
        public IQueryable<DebtDto> Execute(IQueryable<Debt> debts, IQueryable<User> users)
        {
            var userIds = debts.Select(u => u.MustPayId).Distinct().Union(debts.Select(u => u.MustReceiveId).Distinct()).Distinct();
            var userInfos = users.Where(u => userIds.Contains(u.Id)).Select(u => new { Id = u.Id, Name = u.Name });

            var dtos = new List<DebtDto>();
            foreach (var ad in debts)
            {
                dtos.Add(new DebtDto
                {
                    Amount = ad.Amount,
                    MustPayName = userInfos.Single(ui => ui.Id == ad.MustPayId).Name,
                    MustReceiveName = userInfos.Single(ui => ui.Id == ad.MustReceiveId).Name
                });
            }

            return dtos.AsQueryable();
        }
    }

    public interface IDebtsToDebtDtosConverter
    {
        IQueryable<DebtDto> Execute(IQueryable<Debt> debts, IQueryable<User> users);
    }
}
