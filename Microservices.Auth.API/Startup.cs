using System.Text;
using Microservices.Auth.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Microservices.BLL.DataManager.Interfaces;
using Microservices.BLL.DataManager;
using Microservices.ExchangeRates.API;
using Microservices.BLL.Models.Configuration;

namespace Microservices.Auth.API
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
			var appConfig = Configuration.Get<AuthConfig>();
			services.AddDbContext<AuthDBContext>(opt =>
				opt.UseSqlServer(Configuration.GetConnectionString("AuthDBContext"))
			);

			services.AddIdentity<APIUser, IdentityRole>()
				.AddEntityFrameworkStores<AuthDBContext>()
				.AddDefaultTokenProviders();

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

			services.AddMicroserviceDependencies();
			services.AddMvcCore().AddApiExplorer();
			services.AddSwaggerGen();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
