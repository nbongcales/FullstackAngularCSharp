using Excel.FinancialFunctions;
using MoneyMeBackend.DBContext;
using MoneyMeBackend.Models.Requests;
using MoneyMeBackend.Models.Response;
using MoneyMeBackend.Workers.Abstraction;
using System.Collections.Generic;
using System.Diagnostics.Metrics;

namespace MoneyMeBackend.Workers
{
    public class GetQuoteWorker : IGetQuoteWorker
    {
        private readonly MoneyMeDBContext _dbContext;

        public GetQuoteWorker(MoneyMeDBContext dbContext)
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
                var data = new GetQuoteResponse();

                if (getQuote.Customer == null)
                {
                    getQuote.Customer = _dbContext.Customers.FirstOrDefault(c => c.CustomerId == request.CustomerId);
                }

                // get customer information
                data.CustomerId = getQuote.Customer.CustomerId;
                data.Name = getQuote.Customer.FirstName + " " + getQuote.Customer.LastName;
                data.Mobile = getQuote.Customer.Mobile;
                data.Email = getQuote.Customer.Email;

                var term = 1;
                if (getQuote.Term != null)
                {
                    term = Convert.ToInt32(getQuote.Term);
                }

                data.FinanceAmount = Convert.ToDouble(getQuote.AmountRequired);
                data.Term = Convert.ToInt32(getQuote.Term);

                data = CalculateRepayments(data);

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

        private GetQuoteResponse CalculateRepayments(GetQuoteResponse request)
        {
            double amountRequired = request.FinanceAmount;
            int term = request.Term;

            var result = request;

            //rate: The interest rate per period.
            //nper: The total number of payment periods.
            //pv: The present value of the loan or investment.
            //fv: The future value, or the cash balance you want to attain after the last payment is made.
            //    By default, this is set to 0.
            //type: A value indicating when payments are due. Use 0 for payments due at the end of the period,
            //      or 1 for payments due at the beginning of the period.By default, this is set to 0.

            double rate = 0.05396;
            int nper = term;
            double EstablishmentFee = 300;
            double presentValue = amountRequired + EstablishmentFee;
            double futureValue = 0;
            PaymentDue type = PaymentDue.EndOfPeriod;

            double payment = Financial.Pmt(rate / 12, nper, -presentValue, futureValue, type);

            result.FinanceAmount = amountRequired;
            result.Term = term;
            result.RepaymentsFrom = payment;
            result.PaymentType = "Monthly";
            result.EstablishmentFee = EstablishmentFee;
            result.TotalRepayments = (payment * nper);
            result.Interest = result.TotalRepayments - amountRequired - EstablishmentFee;

            return result;
        }
    }
}
