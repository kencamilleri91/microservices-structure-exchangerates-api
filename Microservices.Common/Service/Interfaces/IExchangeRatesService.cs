using System.Net.Http;
using System.Threading.Tasks;

namespace Microservices.BLL.Service.Interfaces
{
	public interface IExchangeRatesService
	{
		Task<HttpResponseMessage> GetAsync(string requestUri);
	}
}