namespace Microservices.Auth.API.Controllers
{
	public class LoginJWTRequestModel
	{
		public string UserName { get; set; }
		
		public string Password { get; set; }
	}
}