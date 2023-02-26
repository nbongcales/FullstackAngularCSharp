using Microsoft.AspNetCore.Mvc;
using MoneyMeBackend.Models.Requests;
using MoneyMeBackend.Models.Response;
using MoneyMeBackend.Workers.Abstraction;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MoneyMeBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoneyMeController : ControllerBase
    {
        private readonly ICalculateQuoteWorker _calculateQuoteWorker;
        private readonly IGetQuoteWorker _getQuoteWorker;
        private readonly IApplyLoanWorker _applyLoanWorker;
        private readonly IEditQuoteWorker _editQuoteWorker;

        public MoneyMeController(
            ICalculateQuoteWorker calculateQuoteWorker,
            IGetQuoteWorker getQuoteWorker,
            IApplyLoanWorker applyLoanWorker,
            IEditQuoteWorker editQuoteWorker)
        {
            this._calculateQuoteWorker = calculateQuoteWorker;
            this._getQuoteWorker = getQuoteWorker;
            this._applyLoanWorker = applyLoanWorker;
            this._editQuoteWorker = editQuoteWorker;
        }

        [HttpPost]
        [Route("CalculateQuote")]
        public async Task<ApiResponse> CalculateQuote([FromBody] CalculateQuoteRequest request)
        {
            return await _calculateQuoteWorker.ExecuteAsync(request);
        }

        [HttpPost]
        [Route("GetQuote")]
        public async Task<ApiResponse> GetQuote([FromBody] GetQuoteRequest request)
        {
            return await _getQuoteWorker.ExecuteAsync(request);
        }

        [HttpPost]
        [Route("ApplyLoan")]
        public async Task<ApiResponse> ApplyLoan([FromBody] ApplyLoanRequest request)
        {
            return await _applyLoanWorker.ExecuteAsync(request);
        }

        [HttpPost]
        [Route("EditQuote")]
        public async Task<ApiResponse> EditQuote([FromBody] GetQuoteRequest request)
        {
            return await _editQuoteWorker.ExecuteAsync(request);
        }

        //// GET: api/<MoneyMeController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<MoneyMeController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<MoneyMeController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<MoneyMeController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<MoneyMeController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
