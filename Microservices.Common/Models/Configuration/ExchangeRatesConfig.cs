using System.Collections.Generic;
using Microservices.BLL.Models.Configuration;

namespace Microservices.ExchangeRates.API
{
	public class ExchangeRatesConfig : BaseConfig
	{
		public ConfigJWT JWT { get; set; }
		public ConfigExchangeRatesDataAPI ExchangeRatesDataAPI { get; set; }
		public Dictionary<string, string> ConnectionStrings { get; set; }

		#region Inner Class
		public class ConfigExchangeRatesDataAPI
		{
			public string Url { get; set; }
			public string Key { get; set; }
			public EndpointsConfig Endpoints { get; set; }

			public class EndpointsConfig
			{
				public string Convert { get; set; } = "convert";
			}
		}
		#endregion
	}
}