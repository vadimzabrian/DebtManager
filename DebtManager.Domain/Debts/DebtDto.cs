namespace DebtManager.Domain.DebtCalculations
{
    public class DebtDto
    {
        public int MustPayId { get; set; }
        public string MustPayName { get; set; }
        public int MustReceiveId { get; set; }
        public string MustReceiveName { get; set; }
        public int Amount { get; set; }
    }
}
