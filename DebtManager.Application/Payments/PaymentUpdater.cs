using DebtManager.Application.Infrastructure;
using DebtManager.Domain.Dtos;
using DebtManager.Domain.Entities;
using System;
using System.Linq;

namespace DebtManager.Application.Payments
{
    public class PaymentUpdater
    {
        IDbRepository _dbRepository;

        public PaymentUpdater(IDbRepository dbRepository)
        {
            _dbRepository = dbRepository;

            if (_dbRepository == null) throw new Exception("dbRepository is null");
        }

        public void Accept(int paymentId, string actionInitializerUsername)
        {
            var actionInitializer = _dbRepository.GetAll<User>().FirstOrDefault(p => p.Username == actionInitializerUsername);
            var existingPayment = _dbRepository.GetAll<Payment>().FirstOrDefault(p => p.Id == paymentId);

            existingPayment.Accept(actionInitializer.Id);
            _dbRepository.PersistChanges();
        }

        public void Reject(int paymentId, string actionInitializerUsername)
        {
            var actionInitializer = _dbRepository.GetAll<User>().FirstOrDefault(p => p.Username == actionInitializerUsername);
            var existingPayment = _dbRepository.GetAll<Payment>().FirstOrDefault(p => p.Id == paymentId);

            existingPayment.Reject(actionInitializer.Id);

            _dbRepository.PersistChanges();
        }

        public void Cancel(int paymentId, string actionInitializerUsername)
        {
            var actionInitializer = _dbRepository.GetAll<User>().FirstOrDefault(p => p.Username == actionInitializerUsername);
            var existingPayment = _dbRepository.GetAll<Payment>().FirstOrDefault(p => p.Id == paymentId);

            existingPayment.Cancel(actionInitializer.Id);

            _dbRepository.PersistChanges();
        }

        public void Resend(int paymentId, string actionInitializerUsername)
        {
            var actionInitializer = _dbRepository.GetAll<User>().FirstOrDefault(p => p.Username == actionInitializerUsername);
            var existingPayment = _dbRepository.GetAll<Payment>().FirstOrDefault(p => p.Id == paymentId);

            existingPayment.Resend(actionInitializer.Id);

            _dbRepository.PersistChanges();
        }
    }
}