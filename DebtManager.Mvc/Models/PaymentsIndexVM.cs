using System.Collections.Generic;

namespace DebtManager.Mvc.Models
{
    public class PaymentsIndexVM
    {
        public IEnumerable<PaymentVM> Payments { get; set; }
        public string LoggedInUsername { get; set; }
    }
}