using System.Linq;
using System.Threading.Tasks;
using Microservices.Auth.Data;
using Microservices.BLL.DataManager;
using Microservices.BLL.DataManager.Interfaces;
using Microservices.BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Microservices.Auth.API.Controllers
{
	[AllowAnonymous]
	[ApiController]
	[Route("api/v1")]
	public class AuthController : ControllerBase
	{
		private readonly ILogger<AuthController> _logger;
		private readonly IAuthManager _authManager;

		public AuthController(ILogger<AuthController> logger, IAuthManager authManager)
		{
			this._logger = logger;

			this._authManager = authManager;
		}

		[HttpGet]
		[AllowAnonymous]
		[Route("health")]
		public string HealthCheck() => "OK";

		/// <summary>
		/// Register an API User. Use /api/v1/login on this microservice to receive a Token that can be used in any other microservice.
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("register")]
		public async Task<OperationResult<bool>> Register([FromBody] CreateAPIUserModel model)
			=> await _authManager.CreateAPIUserAsync(model);

		[HttpPost]
		[Route("login")]
		public async Task<OperationResult<string>> LoginJWT([FromBody] LoginJWTRequestModel model)
			=> await _authManager.RequestJwtTokenAsync(model.UserName, model.Password);
	}
}
