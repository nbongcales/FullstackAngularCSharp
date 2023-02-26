using MoneyMeBackend.Models.Requests;
using MoneyMeBackend.Models.Response;

namespace MoneyMeBackend.Workers.Abstraction
{
    public interface IGetQuoteWorker
    {
        Task<ApiResponse> ExecuteAsync(GetQuoteRequest request);
    }
}
