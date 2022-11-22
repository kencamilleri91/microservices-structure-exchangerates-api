using System;
using System.Threading.Tasks;
using Microservices.BLL.Models;

namespace Microservices.BLL.DataManager.Interfaces
{
	public interface IExchangeRateManager
	{
		Task<OperationResult<ConvertResponseModel>> ConvertAsync(decimal amount, string fromCurrency, string toCurrency);
	}
}