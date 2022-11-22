using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Microservices.Auth.Data
{
	public class AuthDBContext : IdentityDbContext<APIUser>
	{
		public AuthDBContext(DbContextOptions options) : base(options)
		{
		}

		DbSet<APIUser> ApiUsers { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<APIUser>().HasKey(x => x.Id);
		}
	}
}
