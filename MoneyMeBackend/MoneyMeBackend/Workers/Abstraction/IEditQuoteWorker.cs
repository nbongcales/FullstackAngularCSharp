using MoneyMeBackend.Models.Requests;
using MoneyMeBackend.Models.Response;

namespace MoneyMeBackend.Workers.Abstraction
{
    public interface IEditQuoteWorker
    {
        Task<ApiResponse> ExecuteAsync(GetQuoteRequest request);
    }
}
