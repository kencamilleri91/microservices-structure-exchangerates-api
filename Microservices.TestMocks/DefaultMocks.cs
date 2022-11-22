using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using AutoCompare;
using Microservices.Auth.Data;
using Microservices.BLL.DataManager.Interfaces;
using Microservices.BLL.Models;
using Microservices.BLL.Models.Configuration;
using Microservices.BLL.Service;
using Microservices.BLL.Service.Interfaces;
using Microservices.ExchangeRates.API;
using Microservices.ExchangeRates.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace Microservices.TestMocks
{
	public class DefaultMocks
	{
		public const string MOCK_TOKEN = "JWTToken";
		public const string MOCK_URL = "http://localhost:4999";
		public ConvertResponseModel DefaultConvertResponseModel => new ConvertResponseModel
		{
			Date = new DateTime(2022, 11, 20),
			Info = new ConvertResponseModel.InfoModel
			{
				Rate = 1.35m,
			},
			Result = 100,
			Historical = "historical",
			Query = new ConvertResponseModel.QueryModel
			{
				Amount = 100,
				From = "EUR",
				To = "USD",
			},
			Success = true,
		};
		public ExchangeRatesConfig DefaultConfigurationExchangeRates => new ExchangeRatesConfig
		{
			ExchangeRatesDataAPI = new ExchangeRatesConfig.ConfigExchangeRatesDataAPI
			{
				Url = MOCK_URL,
				Key = "api-key",
				Endpoints = new ExchangeRatesConfig.ConfigExchangeRatesDataAPI.EndpointsConfig
				{
					Convert = "convert",
				},
			},
		};

		public ExchangeRatesDBContext DatabaseExchangeRates { get; private set; }
		public AuthDBContext DatabaseAuth { get; private set; }

		public IConfiguration ConfigurationExchangeRates { get; private set; }
		public IConfiguration ConfigurationAuth { get; private set; }

		public Mock<IAuthManager> MockAuthManager { get; private set; }
		public Mock<IExchangeRateManager> MockExchangeRateManager { get; private set; }
		public Mock<IExchangeRatesService> MockExchangeRatesService { get; private set; }

		public DefaultMocks()
		{
			this.DatabaseExchangeRates = new ExchangeRatesDBContext(new DbContextOptionsBuilder<ExchangeRatesDBContext>().UseInMemoryDatabase(nameof(ExchangeRatesDBContext)).Options);
			this.DatabaseAuth = new AuthDBContext(new DbContextOptionsBuilder<AuthDBContext>().UseInMemoryDatabase(nameof(AuthDBContext)).Options);
			SetDefaultMocks();
		}

		public void SetDefaultMocks()
		{
			MockAuthManager ??= new Mock<IAuthManager>();
			MockAuthManager.Setup(x => x.CreateAPIUserAsync(It.IsAny<CreateAPIUserModel>()))
				.ReturnsAsync(true);
			MockAuthManager.Setup(x => x.RequestJwtTokenAsync(It.IsAny<string>(), It.IsAny<string>()))
				.ReturnsAsync(MOCK_TOKEN);

			MockExchangeRateManager ??= new Mock<IExchangeRateManager>();
			this.MockExchangeRateManager.Setup(x => x.ConvertAsync(It.IsAny<decimal>(), It.IsAny<string>(), It.IsAny<string>()))
				.ReturnsAsync(new OperationResult<ConvertResponseModel> { Value = DefaultConvertResponseModel });

			MockExchangeRatesService ??= new Mock<IExchangeRatesService>();

			ConfigurationExchangeRates ??= CreateConfig(DefaultConfigurationExchangeRates);

			ConfigurationAuth ??= CreateConfig(new AuthConfig());
		}

		public ILogger<T> GetLogger<T>()
			=> new Mock<ILogger<T>>().Object;

		public void AssertOperationOkAndNoDifferences<T>(T expectedResultData, OperationResult<T> actualResultData) where T : class
		{
			Assert.IsFalse(actualResultData.Error, $"Expected an OK operation of type {typeof(T).Name}, got an error: {actualResultData.Message}");
			IList<Difference> differences = Comparer.Compare(expectedResultData, actualResultData.Value);
			string differencesStr = string.Join("\n", differences.Select(x => $"{x.Name}: {x.OldValue} != {x.NewValue}"));
			if (differences.Count > 0)
				Assert.Fail($"Operation was OK but result data (type {typeof(T).Name}) was not as expected. Object differences are:\n{differencesStr}");
		}

		public void AssertOperationOkAndEqualToExpected<T>(T expectedResultData, OperationResult<T> actualResultData)
		{
			Assert.IsFalse(actualResultData.Error, $"Expected an OK operation of type {typeof(T).Name}, got an error: {actualResultData.Message}");
			Assert.AreEqual(expectedResultData, actualResultData.Value, $"Operation was OK but result data (type {typeof(T).Name}) was not as expected:\n{expectedResultData}\n\t!=\n {actualResultData}");
		}

		private IList<Difference> CompareClasses<T>(T expectedResultData, T resultData) where T : class
			=> Comparer.Compare(expectedResultData, resultData);

		private IConfiguration CreateConfig<T>(T val) where T : class
			=> new ConfigurationBuilder().AddJsonStream(new MemoryStream(
				Encoding.UTF8.GetBytes(JsonSerializer.Serialize(val))
			)).Build();
	}
}
