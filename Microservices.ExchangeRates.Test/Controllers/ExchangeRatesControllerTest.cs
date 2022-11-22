using Microservices.BLL.Models;
using Microservices.ExchangeRates.API.Controllers;
using Microservices.TestMocks;
using NUnit.Framework;

namespace Microservices.ExchangeRates.Test.Controllers
{
	[TestFixture]
	public class ExchangeRatesControllerTest : BaseTest<ExchangeRatesController, ExchangeRatesControllerTest>
	{
		protected override ExchangeRatesController InitializeTestObject(DefaultMocks mocks)
			=> new ExchangeRatesController(
				Mocks.GetLogger<ExchangeRatesController>(),
				Mocks.DatabaseExchangeRates,
				Mocks.MockExchangeRateManager.Object
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
		public void RetrieveTotalCount_GET_Empty()
		{
			// Arrange & Setup
			Mocks.DatabaseExchangeRates.RemoveRange(Mocks.DatabaseExchangeRates.ExchangeRateLogs);
			// Act & Assert
			int resultData = TestObject.RetrieveTotalCount().Result;
			Assert.That(resultData, Is.EqualTo(0));
		}

		[Test]
		public void RetrieveTotalCount_GET_Populated()
		{
			// Arrange & Setup
			Mocks.DatabaseExchangeRates.RemoveRange(Mocks.DatabaseExchangeRates.ExchangeRateLogs);
			Mocks.DatabaseExchangeRates.Add(Mocks.DatabaseExchangeRates.ExchangeRateLogs.NewTestEntity());
			Mocks.DatabaseExchangeRates.Add(Mocks.DatabaseExchangeRates.ExchangeRateLogs.NewTestEntity());
			Mocks.DatabaseExchangeRates.SaveChanges();
			// Act & Assert
			int resultData = TestObject.RetrieveTotalCount().Result;
			Assert.That(resultData, Is.EqualTo(2));
		}

		[Test]
		public void Convert_POST()
		{
			// Arrange & Setup
			var model = new ConvertRequestModel
			{
				Amount = 1,
				FromCurrency = "EUR",
				ToCurrency = "USD",
			};
			var expectedResultData = Mocks.DefaultConvertResponseModel;
			// Act & Assert
			OperationResult<ConvertResponseModel> resultData = TestObject.Convert(model).Result;
			Mocks.AssertOperationOkAndNoDifferences(expectedResultData, resultData);
			Mocks.MockExchangeRateManager.Verify(x => x.ConvertAsync(model.Amount, model.FromCurrency, model.ToCurrency));
		}
	}
}