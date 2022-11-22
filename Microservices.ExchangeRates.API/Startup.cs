using System.Text;
using Microservices.BLL;
using Microservices.ExchangeRates.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace Microservices.ExchangeRates.API
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			this.Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<ExchangeRatesDBContext>(opt =>
			{
				opt.UseSqlServer(Configuration.GetConnectionString("ExchangeRatesDBContext"));
			});
			services.AddAuthentication(opt =>
			{
				opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(opt =>
			{
				opt.SaveToken = true;
				opt.TokenValidationParameters = new TokenValidationParameters
				{
					ValidAudience = this.Configuration["JWT:ValidAudience"],
					ValidIssuer = this.Configuration["JWT:ValidIssuer"],
					ValidateIssuer = true,
					ValidateAudience = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.Configuration["JWT:Secret"]))
				};
			});
			services.AddAuthorization(opt =>
			{
				opt.AddPolicy("ApiUserJwtAuthPolicy", policy =>
				{
					policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
					policy.RequireAuthenticatedUser();
				});
			});
			services.AddSingleton<IConfiguration>((x) => Configuration);
			services.AddMvcCore().AddApiExplorer();
			services.AddSwaggerGen();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(opt =>
				{
					opt.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
					opt.RoutePrefix = string.Empty;
					opt.EnableTryItOutByDefault();
					opt.ShowExtensions();
				});
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
