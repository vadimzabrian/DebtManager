using DebtManager.Application.Infrastructure;
using DebtManager.Domain.Entities;
using System.Linq;

namespace DebtManager.Application.Payments
{
    public class PaymentsProvider : IPaymentsProvider
    {
        IDbRepository _dbRepository;

        public PaymentsProvider(IDbRepository dbRepository)
        {
            _dbRepository = dbRepository != null ? dbRepository : null;
        }

        public IQueryable<Payment> Execute()
        {
            // ToDo - implement lazy loading
            return _dbRepository.GetAll<Payment>(new[] { "Payer", "Receiver" });
        }
    }

    public interface IPaymentsProvider
    {
        IQueryable<Payment> Execute();
    }
}