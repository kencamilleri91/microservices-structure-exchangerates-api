using System;
using System.Collections.Generic;
using System.Linq;
using AutoCompare;
using Microservices.Auth.Data;
using Microservices.BLL.DataManager;
using Microservices.BLL.DataManager.Interfaces;
using Microservices.BLL.Models;
using Microservices.ExchangeRates.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace Microservices.TestMocks
{
	public class DefaultMocks
	{
		public const string MOCK_TOKEN = "JWTToken";

		public ExchangeRatesDBContext DatabaseExchangeRates { get; set; }
		public AuthDBContext DatabaseAuth { get; set; }

		public Mock<IAuthManager> MockAuthManager { get; set; }

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
		}

		public ILogger<T> GetLogger<T>()
			=> new Mock<ILogger<T>>().Object;

		public void AssertOperationOkAndNoDifferences<T>(OperationResult<T> resultData, T expectedResultData) where T : class
		{
			Assert.IsFalse(resultData.Error, $"Expected an OK operation of type {typeof(T).Name}, got an error: {resultData.Message}");
			if(typeof(T).IsClass)
			{
				IList<Difference> differences = Comparer.Compare(expectedResultData, resultData.Value);
				string differencesStr = string.Join("\n", differences.Select(x => $"{x.Name}: {x.OldValue} != {x.NewValue}"));
				if (differences.Count > 0)
					Assert.Fail($"Operation was OK but result data (type {typeof(T).Name}) was not as expected. Object differences are:\n{differencesStr}");
			}
			else
			{
				Assert.AreEqual(expectedResultData, resultData, $"Operation was OK but result data (type {typeof(T).Name}) was not as expected:\n{expectedResultData}\n\t!=\n {resultData}");
			}
		}

		public void AssertOperationOkAndEqualToExpected<T>(OperationResult<T> resultData, T expectedResultData)
		{
			Assert.IsFalse(resultData.Error, $"Expected an OK operation of type {typeof(T).Name}, got an error: {resultData.Message}");
			Assert.AreEqual(expectedResultData, resultData.Value, $"Operation was OK but result data (type {typeof(T).Name}) was not as expected:\n{expectedResultData}\n\t!=\n {resultData}");
		}

		private IList<Difference> CompareClasses<T>(T expectedResultData, T resultData) where T : class
			=> Comparer.Compare(expectedResultData, resultData);
	}
}
