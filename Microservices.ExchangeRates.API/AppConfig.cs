using System.Collections.Generic;
using Microservices.BLL.Models.Configuration;

namespace Microservices.BLL.Models.Configuration
{
	public class AppConfig : BaseConfig
	{
		public ConfigJWT JWT { get; set; }
		public ConfigExchangeRatesDataAPI ExchangeRatesDataAPI { get; set; }
		public Dictionary<string, string> ConnectionStrings { get; set; }

		#region Inner Class
		public class ConfigExchangeRatesDataAPI
		{
			public string Url { get; set; }
			public string Key { get; set; }
		}
		#endregion
	}
}