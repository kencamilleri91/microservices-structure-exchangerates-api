using System.Net.Http;
using System;
using Microservices.BLL.Service.Interfaces;
using Microservices.ExchangeRates.API;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Microservices.BLL.Service
{
	public class ExchangeRatesService : IExchangeRatesService
	{
		private readonly HttpClient _httpClient;

		public ExchangeRatesService(IConfiguration config)
		{
			var appConfig = config.Get<ExchangeRatesConfig>();
			_httpClient = new HttpClient { BaseAddress = new Uri(appConfig.ExchangeRatesDataAPI.Url) };
			_httpClient.DefaultRequestHeaders.Add("apikey", appConfig.ExchangeRatesDataAPI.Key);
		}

		public virtual async Task<HttpResponseMessage> GetAsync(string requestUri)
			=> await _httpClient.GetAsync(requestUri);
	}
}
