using System.Collections.Specialized;
using System.Text.Json;
using System.Threading.Tasks;
using Microservices.BLL.DataManager.Interfaces;
using Microservices.BLL.Extensions;
using Microservices.BLL.Models;
using Microservices.BLL.Service.Interfaces;
using Microservices.ExchangeRates.API;
using Microsoft.Extensions.Configuration;

namespace Microservices.BLL.DataManager
{
	public class ExchangeRateDataManager : IExchangeRateManager
	{
		private readonly IExchangeRatesService _exchangeRatesService;
		private readonly ExchangeRatesConfig _appConfig;

		public ExchangeRateDataManager(IExchangeRatesService exchangeRatesService, IConfiguration configuration)
		{
			this._exchangeRatesService = exchangeRatesService;
			this._appConfig = configuration.Get<ExchangeRatesConfig>();
		}

		public virtual async Task<OperationResult<ConvertResponseModel>> ConvertAsync(decimal amount, string fromCurrency, string toCurrency)
		{
			var result = new OperationResult<ConvertResponseModel>();

			if (fromCurrency?.Length != 3)
				return result.Fail(AppResource.ERROR_CURRENCY_INVALID, fromCurrency);
			if (toCurrency?.Length != 3)
				return result.Fail(AppResource.ERROR_CURRENCY_INVALID, toCurrency);

			string query = new NameValueCollection
			{
				["amount"] = amount + "",
				["from"] = fromCurrency,
				["to"] = toCurrency
			}.ToUrlQuery();
			var response = await _exchangeRatesService.GetAsync(_appConfig.ExchangeRatesDataAPI.Endpoints.Convert + query);
			if (!response.IsSuccessStatusCode)
				return result.Fail(AppResource.ERROR_EXCHANGE_RATE_API_REQUEST, response.ReasonPhrase);

			return JsonSerializer.Deserialize<ConvertResponseModel>(await response.Content.ReadAsStringAsync());
		}
	}
}
