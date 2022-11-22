using Microservices.Common.Models;

namespace Microservices.ExchangeRates.Data
{
	public class ExchangeRateLog : BaseEntity
	{
		public string UserId { get; set; }
		
		public string To { get; set; }
		
		public string From { get; set; }
		
		public decimal Amount { get; set; }
	}
}