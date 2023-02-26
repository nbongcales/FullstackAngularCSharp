namespace MoneyMeBackend.Models.Requests
{
    public class ApplyLoanRequest
    {
        public int CustomerID { get; set; }
        public double FinanceAmount { get; set; }
        public int Term { get; set; }
        public double RepaymentsFrom { get; set; }
        public string PaymentType { get; set; }
        public double TotalRepayments { get; set; }
        public double EstablishmentFee { get; set; }
        public double Interest { get; set; }
    }
}
