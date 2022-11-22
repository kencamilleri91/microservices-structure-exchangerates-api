using Microservices.BLL.DataManager.Interfaces;
using Microservices.BLL.DataManager;
using Microsoft.Extensions.DependencyInjection;
using Microservices.BLL.Service.Interfaces;
using Microservices.BLL.Service;

namespace Microservices.ExchangeRates.API
{
	public static class DependencyInjection
	{
		public static void AddMicroserviceDependencies(this IServiceCollection services)
		{
			services.AddScoped<IExchangeRateManager, ExchangeRateDataManager>();
			services.AddScoped<IExchangeRatesService, ExchangeRatesService>();
		}
	}
}
