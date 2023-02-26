using System;
using System.Collections.Generic;

namespace MoneyMeBackend.DBContext
{
    public partial class Loan
    {
        public int LoanId { get; set; }
        public int? CustomerId { get; set; }
        public decimal? FinanceAmount { get; set; }
        public int? Term { get; set; }
        public decimal? RepaymentsFrom { get; set; }
        public string? PaymentType { get; set; }
        public decimal? TotalRepayments { get; set; }
        public decimal? EstablishmentFee { get; set; }
        public decimal? Interest { get; set; }

        public virtual Customer? Customer { get; set; }
    }
}
