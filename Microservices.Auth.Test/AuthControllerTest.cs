using Microservices.Auth.API.Controllers;
using Microservices.BLL.DataManager;
using Microservices.BLL.Models;
using Microservices.TestMocks;
using NUnit.Framework;

namespace Microservices.Auth.Test
{
	public class AuthControllerTest : BaseTest<AuthController, AuthControllerTest>
	{
		public override AuthController InitializeTestObject(DefaultMocks mocks)
			=> new AuthController(
				Mocks.GetLogger<AuthController>(),
				Mocks.MockAuthManager.Object
			);

		[SetUp]
		public void Setup()
		{
			Mocks.SetDefaultMocks();
		}

		[Test]
		public void HealthCheck_GET()
		{
			string expectedResultData = "OK";
			string resultData = TestObject.HealthCheck();
			Mocks.AssertOperationOkAndEqualToExpected(expectedResultData, resultData);
		}

		[Test]
		public void Register_POST()
		{
			CreateAPIUserModel model = new CreateAPIUserModel
			{
				Email = "email",
				Password = "password",
				UserName = "name",
			};
			// Arrange & Setup
			bool expectedResultData = true;
			// Act & Assert
			OperationResult<bool> resultData = TestObject.Register(model).Result;
			Mocks.AssertOperationOkAndEqualToExpected(resultData, expectedResultData);
			Mocks.MockAuthManager.Verify(x => x.CreateAPIUserAsync(model));
		}

		[Test]
		public void LoginJWT_POST()
		{
			// Arrange & Setup
			LoginJWTRequestModel model = new LoginJWTRequestModel
			{
				UserName = "name",
				Password = "password",
			};
			string expectedResultData = DefaultMocks.MOCK_TOKEN;
			// Act & Assert
			OperationResult<string> resultData = TestObject.LoginJWT(model).Result;
			Mocks.AssertOperationOkAndEqualToExpected(resultData, expectedResultData);
			Mocks.MockAuthManager.Verify(x => x.RequestJwtTokenAsync(model.UserName, model.Password));
		}
	}
}