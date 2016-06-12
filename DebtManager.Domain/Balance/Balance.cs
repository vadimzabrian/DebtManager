namespace DebtManager.Domain
{
    public class Balance
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public int? MustPayAmount { get; set; }
        public int? MustReceiveAmount { get; set; }

        public int GetBalance()
        {
            if (this.MustReceiveAmount.HasValue) return this.MustReceiveAmount.Value;
            if (this.MustPayAmount.HasValue) return -this.MustPayAmount.Value;

            return 0;
        }
    }
}
