﻿using System;

namespace DebtManager.Domain.Dtos
{
    public class PaymentDto
    {
        public int Id { get; set; }
        public int PayerId { get; set; }
        public string PayerName { get; set; }
        public int ReceiverId { get; set; }
        public string ReceiverName { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }
        public string Reason { get; set; }
    }
}