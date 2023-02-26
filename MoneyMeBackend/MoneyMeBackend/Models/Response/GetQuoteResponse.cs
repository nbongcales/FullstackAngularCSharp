using MoneyMeBackend.DBContext;

namespace MoneyMeBackend.Models.Response
{
    public class GetQuoteResponse
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public double FinanceAmount { get; set; }
        public int Term { get; set; }
        public double RepaymentsFrom { get; set; }
        public string PaymentType { get; set; }
        public double TotalRepayments { get; set; }
        public double EstablishmentFee { get; set; }
        public double Interest { get; set; }

    }
}
