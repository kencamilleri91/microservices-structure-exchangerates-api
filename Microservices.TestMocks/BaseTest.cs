using System;
using System.Linq;
using NUnit.Framework;

namespace Microservices.TestMocks
{
	public abstract class BaseTest<TestedClass, TestSuite>
	{
		protected DefaultMocks Mocks;
		protected TestedClass TestObject;

		public BaseTest()
		{
			Mocks = new DefaultMocks();
			TestObject = InitializeTestObject(Mocks);
		}

		protected abstract TestedClass InitializeTestObject(DefaultMocks mocks);

		[Test]
		public void AllPublicMethodsHaveTest()
		{
			Type testSuiteType = typeof(TestSuite);
			string[] coveredMethodNames = testSuiteType.GetMethods()
				.Where(x => x.IsPublic && x.CustomAttributes.Any(x => x.AttributeType == typeof(TestAttribute)))
				.Select(x => x.Name)
				.ToArray();
			Type testedClassType = typeof(TestedClass);
			string[] methodsToCover = testedClassType.GetMethods()
				.Where(x => x.IsPublic && x.DeclaringType == testedClassType)
				.Select(x => x.Name)
				.ToArray();
			string[] methodsLackingCoverage = methodsToCover
				.Where(methodToCover => !coveredMethodNames.Any(coveredMethod => coveredMethod.StartsWith(methodToCover)))
				.ToArray();
			if (methodsLackingCoverage.Length > 0)
				Assert.Fail($"Type {typeof(TestedClass)} has untested methods:\n\n{string.Join("\n", methodsLackingCoverage)}");
		}
	}
}
