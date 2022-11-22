using System.Threading.Tasks;
using Microservices.BLL.Models;

namespace Microservices.BLL.DataManager.Interfaces
{
	public interface IAuthManager
	{
		Task<OperationResult<bool>> CreateAPIUserAsync(CreateAPIUserModel model);
		Task<OperationResult<string>> RequestJwtTokenAsync(string username, string password);
	}
}