using MoneyMeBackend.DBContext;
using MoneyMeBackend.Models.Requests;
using MoneyMeBackend.Models.Response;
using MoneyMeBackend.Workers.Abstraction;

namespace MoneyMeBackend.Workers
{
    public class ApplyLoanWorker : IApplyLoanWorker
    {
        private readonly MoneyMeDBContext _dbContext;

        public ApplyLoanWorker(MoneyMeDBContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<ApiResponse> ExecuteAsync(ApplyLoanRequest request)
        {
            var result = new ApiResponse();

            if (request != null)
            {
                var loan = new Loan
                {
                    CustomerId = request.CustomerID,
                    FinanceAmount = Convert.ToDecimal(request.FinanceAmount),
                    Term = Convert.ToInt32(request.Term),
                    RepaymentsFrom = Convert.ToInt32(request.RepaymentsFrom),
                    PaymentType = request.PaymentType,
                    TotalRepayments = Convert.ToInt32(request.TotalRepayments),
                    EstablishmentFee = Convert.ToInt32(request.EstablishmentFee),
                    Interest = Convert.ToInt32(request.Interest)
                };

                _dbContext.Loans.Add(loan);
                _dbContext.SaveChanges();

                result.status = "success";
                result.message = "Loan Success!";
            }

            return result;
        }
    }
}
