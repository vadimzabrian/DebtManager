namespace DebtManager.Domain
{
    public class Balance
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public int? MustPayAmount { get; set; }
        public int? MustReceiveAmount { get; set; }
    }
}
