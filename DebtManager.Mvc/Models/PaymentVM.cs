﻿using System;
using System.ComponentModel.DataAnnotations;
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
        [Range(1, 10000)]
        public int Amount { get; set; }
        public DateTime Date { get; set; }
        public string Reason { get; set; }
        public int Status { get; set; }
        public string StatusName { get; set; }
        public DateTime? AcceptedDate { get; set; }
        public int PayerBalance { get; set; }
        public int ReceiverBalance { get; set; }

        public SelectList Users { get; set; }
    }
}