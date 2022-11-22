using Microservices.BLL.DataManager.Interfaces;
using Microservices.BLL.DataManager;
using Microsoft.Extensions.DependencyInjection;

namespace Microservices.Auth.API
{
	public static class DependencyInjection
	{
		public static void AddMicroserviceDependencies(this IServiceCollection services)
		{
			services.AddScoped<IAuthManager, AuthManager>();
		}
	}
}
