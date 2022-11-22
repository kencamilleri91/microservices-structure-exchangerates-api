using Microservices.BLL.Models.Configuration;
using System.Collections.Generic;

namespace Microservices.Auth.API
{
	public class AppConfig : BaseConfig
	{
		public ConfigJWT JWT { get; set; }
		public Dictionary<string, string> ConnectionStrings { get; set; }
	}
}
