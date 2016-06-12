using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DebtManager.Mvc.Models
{
    public class DashboardVM
    {
        public IEnumerable<DebtVM> Debts { get; set; }
        public BalanceVM Balance { get; set; }
        public string LoggedInUsername { get; set; }
    }
}