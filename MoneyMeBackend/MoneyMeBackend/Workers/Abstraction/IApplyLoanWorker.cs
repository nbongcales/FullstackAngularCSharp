using MoneyMeBackend.Models.Requests;
using MoneyMeBackend.Models.Response;

namespace MoneyMeBackend.Workers.Abstraction
{
    public interface IApplyLoanWorker
    {
        Task<ApiResponse> ExecuteAsync(ApplyLoanRequest request);
    }
}
