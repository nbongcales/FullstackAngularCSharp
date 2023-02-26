using System;
using System.Collections.Generic;

namespace MoneyMeBackend.DBContext
{
    public partial class Quote
    {
        public int QuoteId { get; set; }
        public int? CustomerId { get; set; }
        public decimal? AmountRequired { get; set; }
        public int? Term { get; set; }

        public virtual Customer? Customer { get; set; }
    }
}
