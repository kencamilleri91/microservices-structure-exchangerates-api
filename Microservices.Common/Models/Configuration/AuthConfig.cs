using System.Collections.Generic;

namespace Microservices.BLL.Models.Configuration
{
	public class AuthConfig : BaseConfig
	{
		public ConfigJWT JWT { get; set; }
		public Dictionary<string, string> ConnectionStrings { get; set; }
	}
}
