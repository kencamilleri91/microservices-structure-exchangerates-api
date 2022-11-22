using AutoFixture;
using Microsoft.EntityFrameworkCore;

namespace Microservices.TestMocks
{
	public static class DBContextExtensions
	{
		public static Fixture fixture;
		static DBContextExtensions()
		{
			fixture = new Fixture();
		}
		public static T NewTestEntity<T>(this DbSet<T> set) where T : class
			=> fixture.Create<T>();
	}
}
