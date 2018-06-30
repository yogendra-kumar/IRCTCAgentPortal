using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Mpower.Data;
using Mpower.Services;
using PaulMiami.AspNetCore.Mvc.Recaptcha;

namespace Mpower.Travel.Web.Admin
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see https://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddOptions();
            services.Configure<ApplicationSetting>(Configuration.GetSection("ApplicationSetting"));    
            services.AddRecaptcha(new RecaptchaOptions
            {
                SiteKey = Configuration["Recaptcha:SiteKey"],
                SecretKey = Configuration["Recaptcha:SecretKey"]
            });
            switch (Configuration.GetConnectionString("provider").ToLower())
            {
                case "sqlserver":
                    services.AddDbContext<ApplicationDbContext>(
                         options =>
                         options.UseSqlServer(Configuration.GetConnectionString("ConnectionString")));
                    break;
                case "postgres":
                    services.AddDbContext<ApplicationDbContext>(options =>
                       options.UseNpgsql(Configuration.GetConnectionString("ConnectionString")));
                    break;
                case "sqlite":
                    services.AddDbContext<ApplicationDbContext>(options =>
                       options.UseSqlite(Configuration.GetConnectionString("ConnectionString")));
                    break;
            }

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
                services.AddSession(options => { 
            options.IdleTimeout = TimeSpan.FromMinutes(30); 
            options.CookieName = ".CMSApplication";
        });
            services.AddMvc();

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Admin/Error");
            }
            app.UseSession();
            app.UseStaticFiles();
            app.UseIdentity();

            // Add external authentication middleware below. To configure them please see https://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=default}/{id?}");

                routes.MapRoute(
                   name: "Admin",
                   template: "{controller=Admin}/{action=Index}/{id?}");

                routes.MapRoute(
                   name: "Page",
                   template: "{controller=Admin}/{action=PageEdit}/{id?}");
            });
        }
    }
}
