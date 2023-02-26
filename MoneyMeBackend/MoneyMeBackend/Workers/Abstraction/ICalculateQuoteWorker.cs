using MoneyMeBackend.Models.Requests;
using MoneyMeBackend.Models.Response;

namespace MoneyMeBackend.Workers.Abstraction
{
    public interface ICalculateQuoteWorker
    {
        Task<ApiResponse> ExecuteAsync(CalculateQuoteRequest request);
    }
}
