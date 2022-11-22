using System.Threading.Tasks;
using Microservices.BLL;
using Microservices.BLL.DataManager.Interfaces;
using Microservices.BLL.Models;
using Microservices.ExchangeRates.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace Microservices.ExchangeRates.API.Controllers
{
	/// <summary>
	/// Integrates with Exchange Rates Data API available at https://apilayer.com/marketplace/exchangerates_data-api
	/// </summary>
	[Authorize]
	[ApiController]
	[Route("api/v1")]
	public class ExchangeRatesController : ControllerBase
	{
		private readonly ILogger<ExchangeRatesController> _logger;
		private readonly ExchangeRatesDBContext _exchangeRatesDBContext;
		private readonly IExchangeRateManager _exchangeRateManager;

		public ExchangeRatesController(ILogger<ExchangeRatesController> logger, ExchangeRatesDBContext exchangeRatesDBContext, IExchangeRateManager exchangeRateManager)
		{
			this._logger = logger;
			this._exchangeRatesDBContext = exchangeRatesDBContext;
			this._exchangeRateManager = exchangeRateManager;
		}

		[HttpGet]
		[AllowAnonymous]
		[Route("health")]
		public OperationResult<string> HealthCheck() => "OK";

		[HttpGet]
		[Route("exchangeRateLogs/count")]
		public async Task<OperationResult<int>> RetrieveTotalCount()
		{
			System.Collections.Generic.List<ExchangeRateLog> records
				= await _exchangeRatesDBContext.ExchangeRateLogs.ToListAsync();
			return records.Count;
		}

		[HttpPost]
		[Route("convert")]
		public async Task<OperationResult<ConvertResponseModel>> Convert([FromBody] ConvertRequestModel model)
		{
			if (model == null)
				return new OperationResult<ConvertResponseModel>().Fail(AppResource.ERROR_MODEL_INVALID);

			ConvertResponseModel response = await _exchangeRateManager.ConvertAsync(model.Amount, model.FromCurrency, model.ToCurrency);
			return response;
		}
	}
}
