
namespace DebtManager.Mvc.Models
{
    public class BalanceVM
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public int? MustPayAmount { get; set; }
        public int? MustReceiveAmount { get; set; }
    }
}