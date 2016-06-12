using DebtManager.Application.Infrastructure;
using DebtManager.Domain.Dtos;
using DebtManager.Domain.Entities;
using System.Linq;

namespace DebtManager.Application
{
    public class PaymentCreator : IPaymentCreator
    {
        IDbRepository _dbRepository;

        public PaymentCreator(IDbRepository dbRepository)
        {
            _dbRepository = dbRepository != null ? dbRepository : null;
        }

        public Payment Execute(PaymentDto dto)
        {
            var payment = Payment.FromDto(dto, _dbRepository.GetAll<User>().First(u => u.Username == dto.PayerUsername), _dbRepository.GetAll<User>().First(u => u.Id == dto.ReceiverId));

            _dbRepository.Add(payment);

            _dbRepository.PersistChanges();

            return payment;
        }
    }

    public interface IPaymentCreator
    {
        Payment Execute(PaymentDto dto);
    }
}