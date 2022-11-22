using System.Linq;
using System.Threading.Tasks;
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

		public ExchangeRatesController(ILogger<ExchangeRatesController> logger, ExchangeRatesDBContext exchangeRatesDBContext)
		{
			this._logger = logger;
			this._exchangeRatesDBContext = exchangeRatesDBContext;
		}

		[HttpGet]
		[AllowAnonymous]
		[Route("health")]
		public string HealthCheck() => "OK";

		[HttpGet]
		[Route("exchangeRateLogs/count")]
		public async Task<int> RetrieveTotalCount()
		{
			System.Collections.Generic.List<ExchangeRateLog> records
				= await _exchangeRatesDBContext.ExchangeRateLogs.ToListAsync();
			return records.Count;
		}
	}
}
