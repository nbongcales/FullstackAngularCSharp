using MoneyMeBackend.Models.Requests;

namespace MoneyMeBackend.Models.Response
{
    public class EditQuoteResponse : CalculateQuoteRequest
    {
        public int QuoteId { get; set; }
        public int CustomerId { get; set; }
    }
}
