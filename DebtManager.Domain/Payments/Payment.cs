using DebtManager.Domain.Dtos;
using DebtManager.Domain.Payments;
using System;
using System.Linq;
using Vadim.Common;

namespace DebtManager.Domain.Entities
{
    public class Payment : IEntity<int>
    {
        public int Id { get; private set; }
        public User Payer { get; private set; }
        public User Receiver { get; private set; }
        public int Amount { get; private set; }
        public DateTime Date { get; private set; }
        public string Reason { get; private set; }
        public int Status { get; private set; }
        public DateTime? AcceptedDate { get; private set; }
        public int PayerBalance { get; private set; }
        public int ReceiverBalance { get; private set; }

        private Payment()
        {
            this.Status = (int)PaymentStatus.Pending;
        }

        public void Confirm(int actionInitiatorId, IBalanceCalculator balanceCalculator, IQueryable<Payment> payments)
        {
            #region validations
            if (this.Status != (int)PaymentStatus.Pending) throw new Exception("Invalid operation. A payment can go to Active state only from Pending.");
            if (this.Receiver.Id != actionInitiatorId) throw new Exception("User not authorized to perform the operation.");
            #endregion

            this.Status = (int)PaymentStatus.Confirmed;
            this.AcceptedDate = DateTime.UtcNow;

            this.PayerBalance = balanceCalculator.ExecuteFor(this.Payer.Id, payments).GetBalance();
            this.ReceiverBalance = balanceCalculator.ExecuteFor(this.Receiver.Id, payments).GetBalance();      
        }

        public void Reject(int actionInitiatorId)
        {
            #region validations
            if (this.Status != (int)PaymentStatus.Pending) throw new Exception("Invalid operation. A payment can go to Rejected state only from Pending.");
            if (this.Receiver.Id != actionInitiatorId) throw new Exception("User not authorized to perform the operation.");
            #endregion

            this.Status = (int)PaymentStatus.Rejected;
        }

        public void Resend(int actionInitiatorId)
        {
            #region validations
            if (this.Status != (int)PaymentStatus.Rejected) throw new Exception("Invalid operation. A payment can be put back to Pending state only from Rejected.");
            if (this.Payer.Id != actionInitiatorId) throw new Exception("User not authorized to perform the operation.");
            #endregion

            this.Status = (int)PaymentStatus.Pending;
        }

        public void Delete(int actionInitiatorId)
        {
            #region validations
            if (!(this.Status == (int)PaymentStatus.Pending || this.Status == (int)PaymentStatus.Rejected)) throw new Exception("Invalid operation. A payment can Canceled only when in Pending or Rejected state.");
            if (this.Payer.Id != actionInitiatorId) throw new Exception("User not authorized to perform the operation.");
            #endregion

            this.Status = (int)PaymentStatus.Delete;
        }

        public void Neutralize(PaymentDto newPaymentDto, int? actionInitiatorId = null)
        {
            #region validations
            if (this.Status != (int)PaymentStatus.Confirmed) throw new Exception("Invalid operation. A payment can go to Neutralized state only from Active.");
            #endregion

            this.Status = newPaymentDto.Status;
        }


        public PaymentDto ToDto()
        {
            PaymentDto dto = new PaymentDto();

            dto.Id = this.Id;
            dto.PayerId = this.Payer != null ? this.Payer.Id : 0;
            dto.PayerName = this.Payer != null ? this.Payer.Name : String.Empty;
            dto.ReceiverId = this.Receiver != null ? this.Receiver.Id : 0;
            dto.ReceiverName = this.Receiver != null ? this.Receiver.Name : String.Empty;
            dto.Amount = this.Amount;
            dto.Date = this.Date;
            dto.Reason = this.Reason;
            dto.Status = this.Status;

            return dto;
        }

        public static Payment FromDto(PaymentDto dto, User payer, User receiver)
        {
            if (payer.Id == receiver.Id) throw new Exception("Payer and Receiver must not be the same person.");
            if (dto.Amount <= 0) throw new Exception("Amount should be greater than 0.");

            Payment entity = new Payment();

            entity.Id = dto.Id;
            entity.Payer = payer;
            entity.Receiver = receiver;
            entity.Amount = dto.Amount;
            entity.Date = DateTime.Now;
            entity.Reason = dto.Reason;
            entity.Status = dto.Status;

            return entity;
        }

        // ToDo - rafactor this shit out
        public static string GetStatusNameFor(int p)
        {
            switch (p)
            {
                case 0: return "Pending";
                case 1: return "Active";
                case 2: return "Rejected";
                case 3: return "Neutralized";
                case 4: return "Canceled";
                default: return String.Empty;
            }
        }
    }
}