using MoneyMeBackend.DBContext;
using MoneyMeBackend.Models.Requests;
using MoneyMeBackend.Models.Response;
using MoneyMeBackend.Workers.Abstraction;

namespace MoneyMeBackend.Workers
{
    public class EditQuoteWorker : IEditQuoteWorker
    {
        private readonly MoneyMeDBContext _dbContext;

        public EditQuoteWorker(MoneyMeDBContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<ApiResponse> ExecuteAsync(GetQuoteRequest request)
        {
            var result = new ApiResponse();

            if (request == null)
            {
                result.status = "error";
                result.message = "Invalid request!";
                return result;
            }

            var getQuote = _dbContext.Quotes.FirstOrDefault(l => l.QuoteId == request.QuoteId && l.CustomerId == request.CustomerId);

            if (getQuote != null)
            {
                var customer = _dbContext.Customers.FirstOrDefault(c => c.CustomerId == request.CustomerId);
                var data = new EditQuoteResponse();

                if (customer != null)
                {
                    data = new EditQuoteResponse
                    {
                        QuoteId = getQuote.QuoteId,
                        CustomerId = customer.CustomerId,
                        AmountRequired = getQuote.AmountRequired.ToString(),
                        Term = getQuote.Term.ToString(),
                        Title = customer.Title,
                        FirstName = customer.FirstName,
                        LastName = customer.LastName,
                        DateOfBirth = customer.DateOfBirth?.ToString("yyyy-MM-dd"),
                        Mobile = customer.Mobile,
                        Email = customer.Email
                    };
                }

                // get customer information
                result.status = "success";
                result.message = "";
                result.data = data;

                return result;
            }
            else
            {
                //result = "Loan not found!";
                return result;
            }
        }
    }
}
