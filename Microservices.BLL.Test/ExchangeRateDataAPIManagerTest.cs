using System;
using System.Collections.Specialized;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microservices.BLL.DataManager;
using Microservices.BLL.Extensions;
using Microservices.BLL.Models;
using Microservices.TestMocks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Microservices.BLL.Test
{
	[TestFixture]
	public class ExchangeRateDataAPIManagerTest : BaseTest<ExchangeRateDataManager, ExchangeRateDataAPIManagerTest>
	{
		protected override ExchangeRateDataManager InitializeTestObject(DefaultMocks mocks)
			=> new ExchangeRateDataManager(
				mocks.MockExchangeRatesService.Object,
				mocks.ConfigurationExchangeRates
			);

		[Test]
		public void ConvertAsync()
		{
			// Arrange & Setup
			decimal amount = 2;
			string fromCurrency = "USD";
			string toCurrency = "EUR";
			var expectedResultData = Mocks.DefaultConvertResponseModel;
			string expectedUrl = Mocks.DefaultConfigurationExchangeRates.ExchangeRatesDataAPI.Endpoints.Convert + new NameValueCollection
			{
				["amount"] = amount + "",
				["from"] = fromCurrency,
				["to"] = toCurrency
			}.ToUrlQuery();

			Mocks.MockExchangeRatesService.Setup(x => x.GetAsync(It.IsAny<string>()))
				.Returns(Task.FromResult(new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.OK)
				{
					Content = new StringContent(JsonSerializer.Serialize(Mocks.DefaultConvertResponseModel)),
				}));

			// Act & Assert
			OperationResult<ConvertResponseModel> resultData = TestObject.ConvertAsync(amount, fromCurrency, toCurrency).Result;
			Mocks.AssertOperationOkAndNoDifferences(expectedResultData, resultData);
			Mocks.MockExchangeRatesService.Verify(x => x.GetAsync(expectedUrl));
		}
	}
}