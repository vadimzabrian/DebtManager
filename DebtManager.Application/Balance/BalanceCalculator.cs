using DebtManager.Application.Infrastructure;
using DebtManager.Domain;
using DebtManager.Domain.Entities;
using DebtManager.Domain.Payments;
using System.Linq;

namespace DebtManager.Application
{

    public class BalanceCalculator : IBalanceCalculator
    {
        IDbRepository _dbRepository;
        DebtManager.Domain.IBalanceCalculator _domainBalanceCalculator;

        public BalanceCalculator(IDbRepository dbRepository, DebtManager.Domain.IBalanceCalculator domainBalanceCalculator)
        {
            _dbRepository = dbRepository != null ? dbRepository : null;
            _domainBalanceCalculator = domainBalanceCalculator;
        }

        public Balance ExecuteFor(string username)
        {
            var payments = _dbRepository.GetAll<Payment>().Where(p => p.Status == (int)PaymentStatus.Active);
            var user = _dbRepository.GetAll<User>().First(u => u.Username == username);

            return _domainBalanceCalculator.ExecuteFor(user.Id, payments);
        }
    }

    public interface IBalanceCalculator
    {
        Balance ExecuteFor(string username);
    }
}
