using System.Threading.Tasks;
using Microservices.Auth.Data;
using Microservices.BLL.DataManager.Interfaces;
using Microservices.BLL.Models;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using System;
using System.Linq;

namespace Microservices.BLL.DataManager
{
	public class AuthManager : IAuthManager
	{
		private readonly UserManager<APIUser> _userManager;
		private readonly IConfiguration _configuration;

		public AuthManager(UserManager<APIUser> applicationUserManager, IConfiguration configuration)
		{
			this._userManager = applicationUserManager;
			this._configuration = configuration;
		}

		async Task<OperationResult<bool>> IAuthManager.CreateAPIUserAsync(CreateAPIUserModel model)
		{
			OperationResult<bool> result = new OperationResult<bool>();
			
			var user = await _userManager.FindByNameAsync(model.UserName);
			if (user != null)
				return result.Fail(AppResource.ERROR_USER_ALREADY_EXISTS);

			IdentityResult identityResult = await _userManager.CreateAsync(new APIUser
			{
				UserName = model.UserName,
				Email = model.Email,
			}, model.Password);
			if (identityResult.Errors?.Count() > 0)
				return result.Fail(AppResource.ERROR_CREATE_USER, string.Join("\n", identityResult.Errors.Select(x => x.Description)));

			return true;
		}

		async Task<OperationResult<string>> IAuthManager.RequestJwtTokenAsync(string username, string password)
		{
			OperationResult<string> result = new OperationResult<string>();

			var user = await _userManager.FindByNameAsync(username);
			if (user == null)
				return result.Fail(AppResource.ERROR_USER_NOT_FOUND);

			var ok = await _userManager.CheckPasswordAsync(user, password);
			if (!ok)
				return result.Fail(AppResource.ERROR_INVALID_CREDENTIALS);

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new[]
				{
					new Claim(JwtRegisteredClaimNames.NameId, user.Id),
					new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
					new Claim(JwtRegisteredClaimNames.Email, user.Email),
					new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
				 }),
				Expires = DateTime.UtcNow.AddMinutes(5),
				Issuer = _configuration["JWT:ValidIssuer"],
				Audience = _configuration["JWT:ValidAudience"],
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"])), SecurityAlgorithms.HmacSha512Signature)
			};
			var tokenHandler = new JwtSecurityTokenHandler();
			var token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
		}
	}
}
