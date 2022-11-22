using System.Linq;
using Microservices.ExchangeRates.API.Controllers;
using Microservices.TestMocks;
using NUnit.Framework;

namespace Microservices.ExchangeRates.Test.Controllers
{
	public class ExchangeRatesControllerTest : BaseTest<ExchangeRatesController, ExchangeRatesControllerTest>
	{
		public override ExchangeRatesController InitializeTestObject(DefaultMocks mocks)
			=> new ExchangeRatesController(
				Mocks.GetLogger<ExchangeRatesController>(),
				Mocks.DatabaseExchangeRates
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

	}
}