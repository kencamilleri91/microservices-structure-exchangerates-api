namespace Microservices.BLL.Models.Configuration
{
	public class ConfigJWT
	{
		public string ValidAudience { get; set; }
		public string ValidIssuer { get; set; }
		public string Secret { get; set; }
	}
}