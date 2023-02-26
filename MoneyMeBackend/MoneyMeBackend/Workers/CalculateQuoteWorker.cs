using Microsoft.EntityFrameworkCore;
using MoneyMeBackend.DBContext;
using MoneyMeBackend.Models.Requests;
using MoneyMeBackend.Models.Response;
using MoneyMeBackend.Workers.Abstraction;

namespace MoneyMeBackend.Workers
{
    public class CalculateQuoteWorker : ICalculateQuoteWorker
    {
        private readonly MoneyMeDBContext _dbContext;

        public CalculateQuoteWorker(MoneyMeDBContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<ApiResponse> ExecuteAsync(CalculateQuoteRequest request)
        {
            var result = new ApiResponse();
            string errorMsg = string.Empty;

            if (!Validate(request, out errorMsg))
            {
                result.status = "error";
                result.message = errorMsg;
                return result;
            }

            // check if applicant is already existing
            var currentCustomer = _dbContext.Customers.FirstOrDefault(r => r.FirstName == request.FirstName
                                                                    && r.LastName == request.LastName
                                                                    && r.DateOfBirth == Convert.ToDateTime(request.DateOfBirth)
                                                                    && r.Mobile == request.Mobile
                                                                    && r.Email == request.Email);
            if (currentCustomer == null)
            {
                // save Customer details
                var newCustomer = new Customer
                {
                    Title = request.Title,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    DateOfBirth = Convert.ToDateTime(request.DateOfBirth),
                    Mobile = request.Mobile,
                    Email = request.Email
                };

                _dbContext.Customers.Add(newCustomer);
                _dbContext.SaveChanges();
                currentCustomer = newCustomer;
            }


            var checkQuote = _dbContext.Quotes.FirstOrDefault(q => q.AmountRequired == Convert.ToDecimal(request.AmountRequired) &&
                                                                q.Term == Convert.ToInt32(request.Term) &&
                                                                q.CustomerId == currentCustomer.CustomerId);

            if (checkQuote == null)
            {
                //Create new loan
                var newQuote = new Quote
                {
                    CustomerId = currentCustomer.CustomerId,
                    AmountRequired = Convert.ToDecimal(request.AmountRequired),
                    Term = Convert.ToInt32(request.Term)
                };

                // save loan details
                _dbContext.Quotes.Add(newQuote);
                _dbContext.SaveChanges();
                checkQuote = newQuote;
            }

            result.status = "success";
            result.message = "/apply-now?QuoteId=" + checkQuote.QuoteId + "&CustomerId=" + currentCustomer.CustomerId;

            return result;
        }

        protected bool Validate(CalculateQuoteRequest request, out string errorMsg)
        {
            errorMsg = string.Empty;

            if (!ValidateAge(request.DateOfBirth, out errorMsg))
            {
                return false;
            }

            //validate if mobile is blacklisted
            //validate if email is blacklisted

            return true;
        }

        protected bool ValidateAge(string dateOfBirth, out string errorMsg)
        {
            errorMsg = string.Empty;

            if (dateOfBirth == string.Empty)
            {
                errorMsg = "Invalid date of birth!";
                return false;
            }

            DateTime DateOfBirth = Convert.ToDateTime(dateOfBirth);
            int age = DateTime.Today.Year - DateOfBirth.Year;

            // subtract a year if the birthdate hasn't occurred yet this year
            if (DateTime.Today.Month < DateOfBirth.Month || (DateTime.Today.Month == DateOfBirth.Month && DateTime.Today.Day < DateOfBirth.Day))
            {
                age--;
            }

            if (age < 18)
            {
                errorMsg = "The applicant should be above 18 years old!";
                return false;
            }

            return true;
        }


    }
}
