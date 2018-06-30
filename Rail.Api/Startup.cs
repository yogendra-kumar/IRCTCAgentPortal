using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Mpower.Rail.Api.TokenProvider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using System;
using Mpower.Rail.Data;
using Microsoft.EntityFrameworkCore;
using Mpower.Rail.Data.IRepository;
using Mpower.Rail.Data.Repository;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Mpower.Rail.Model.ViewModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Mpower.Rail.NGETSystem.Processor.BookingServices;
using Mpower.Rail.NGETSystem.Processor.EnquiryServices;

namespace Mpower.Rail.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddJsonFile("connectionstrings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }
        private const string SecretKey = "needtogetthisfromenvironment";
        private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.AddCors();

            // services.AddDbContext<ApplicationDbContext>(options =>
            //             options.UseNpgsql(Configuration.GetConnectionString("ConnectionString"),
            //             b => b.MigrationsAssembly("MPower.Rail.Api")));

            switch (Configuration.GetConnectionString("provider").ToLower())
            {
                case "sqlserver":
                    services.AddDbContext<ApplicationDbContext>(
                         options =>
                         options.UseSqlServer(Configuration.GetConnectionString("ConnectionString"),b =>
 b.MigrationsAssembly("Mpower.Rail.Api")));
                        
                    break;
                case "postgres":
                    services.AddDbContext<ApplicationDbContext>(options =>
                       options.UseNpgsql(Configuration.GetConnectionString("ConnectionString"),b =>
 b.MigrationsAssembly("Mpower.Rail.Api")));
                       
                    break;
                case "sqlite":
                    services.AddDbContext<ApplicationDbContext>(options =>
                       options.UseSqlite(Configuration.GetConnectionString("ConnectionString"),b =>
 b.MigrationsAssembly("Mpower.Rail.Api")));
                      
                    break;
            }

            //Add IdentityDBContext
            services.AddIdentity<UserViewModel, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
                
            // Repositories
            services.AddScoped<IMerchantRepository, MerchantRepository>();            
            services.AddScoped<IApplicationErrorRepository, ApplicationErrorsRepository>();
            services.AddScoped<IBooking,Booking>();
            services.AddScoped<IEnquiry,Enquiry>();
            //services.Configure<ApplicationSetting>(Configuration.GetSection("ApplicationSetting"));
            // Make authentication compulsory across the board (i.e. shut
            // down EVERYTHING unless explicitly opened up).
            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                           .RequireAuthenticatedUser()
                           .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });

            // Use policy auth.
            services.AddAuthorization(options =>
            {
                options.AddPolicy("MpowerRailAgent", policy => policy.RequireClaim("Mpower.Oxigen.Rail", "Agent"));
            });

            // Get options from app settings
            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));

            // Configure JwtIssuerOptions
            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                if (Convert.ToDouble(jwtAppSettingOptions[nameof(JwtIssuerOptions.ValidFor)]) > 0)
                {
                    options.ValidFor = TimeSpan.FromMinutes(Convert.ToDouble(jwtAppSettingOptions[nameof(JwtIssuerOptions.ValidFor)]));
                }
                options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            });
            //services.AddMvc();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            
            app.UseStaticFiles();
            app.UseIdentity();
            
            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

                ValidateAudience = true,
                ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _signingKey,

                RequireExpirationTime = true,
                ValidateLifetime = true,

                ClockSkew = TimeSpan.Zero
            };

            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = tokenValidationParameters
            });
            
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            app.UseMvc();
            app.UseExceptionHandler(
                builder =>
                {
                    builder.Run(
                        async context =>
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                            context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                            var error = context.Features.Get<IExceptionHandlerFeature>();
                            if (error != null)
                            {
                                //context.Response.AddApplicationError(error.Error.Message);
                                await context.Response.WriteAsync(error.Error.Message).ConfigureAwait(false);
                            }
                        });
                });
        }
    }
}
