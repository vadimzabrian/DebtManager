﻿using System;
using System.Web.Mvc;
namespace DebtManager.Mvc.Models
{
    public class PaymentVM
    {
        public int Id { get; set; }
        public int PayerId { get; set; }
        public string PayerName { get; set; }
        public string PayerUsername { get; set; }
        public int ReceiverId { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverUsername { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }
        public string Reason { get; set; }
        public int Status { get; set; }
        public string StatusName { get; set; }

        public SelectList Users { get; set; }
    }
}