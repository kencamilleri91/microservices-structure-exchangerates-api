using Microservices.BLL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.BLL.Extensions
{
	public static class OperationResultExtensions
	{
		public static OperationResult<T> Fail<T>(this ControllerBase controller, string message, params string[] stringFormats)
		{
			return new OperationResult<T>().Fail(message, stringFormats);
		}
	}
}
