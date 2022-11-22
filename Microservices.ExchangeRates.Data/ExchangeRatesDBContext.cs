using Microsoft.EntityFrameworkCore;

namespace Microservices.ExchangeRates.Data
{
	public class ExchangeRatesDBContext : DbContext
	{
		public ExchangeRatesDBContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<ExchangeRateLog> ExchangeRateLogs { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<ExchangeRateLog>().HasKey(e => e.Id);
		}
	}
}
