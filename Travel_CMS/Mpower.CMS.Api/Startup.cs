using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Mpower.Data.Repository;
using Mpower.Data;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;

namespace Mpower.CMS.Api
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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.AddCors();
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

            // services.AddIdentity<ApplicationUser, IdentityRole>()
            //     .AddEntityFrameworkStores<ApplicationDbContext>()
            //     .AddDefaultTokenProviders();


            services.Configure<ApiSetting>(Configuration.GetSection("ApiSetting"));

            // Add application services.
            services.AddScoped<IApplicationRepository, ApplicationRepository>();
            services.AddScoped<IApplication_SettingsRepository, Application_SettingsRepository>();
            services.AddScoped<IApplication_LayoutsRepository, Application_LayoutsRepository>();
            services.AddScoped<IApplication_ErrorsRepository, Application_ErrorsRepository>();
            services.AddScoped<IApplication_ResponseCodesRepository, Application_ResponseCodesRepository>();
            services.AddScoped<IApplication_PagePansRepository, Application_PagePansRepository>();
            services.AddScoped<IApplication_PagesRepository, Application_PagesRepository>();
            services.AddScoped<IApplication_FilesRepository, Application_FilesRepository>();
            services.AddScoped<IApplication_ViewModelRepository, Application_ViewModelRepository>();
            services.AddScoped<IApplication_PageViewModelRepository, Application_PageViewModelRepository>();
            services.AddScoped<IPages_MetadataRepository,Pages_MetadataRepository>();
            services.AddScoped<IClient_ConfigurationRepository,Client_ConfigurationRepository>();
            services.AddScoped<IApplication_ErrorViewModelRepository,Application_ErrorViewModelRepository>();
            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            //*******Enable file server and file folders mappings***********
            if (Configuration.GetSection("ApiSetting").GetValue<bool>("EnableFileServer"))
            {
                if (!new System.IO.DirectoryInfo(env.ContentRootPath + "/" + Configuration.GetSection("ApiSetting").GetValue<string>("FileServerFolder")).Exists)
                {
                    System.IO.Directory.CreateDirectory(env.ContentRootPath + "/" + Configuration.GetSection("ApiSetting").GetValue<string>("FileServerFolder"));
                }
                app.UseStaticFiles(new StaticFileOptions()
                {
                    FileProvider = new PhysicalFileProvider(
                                                    System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(),
                                                    @Configuration.GetSection("ApiSetting").GetValue<string>("FileServerFolder"))
                                                    ),
                    RequestPath = new PathString("/" + Configuration.GetSection("ApiSetting").GetValue<string>("FileServerFolder")),
                    //ContentTypeProvider = provider
                });
                app.UseFileServer(new FileServerOptions()
                {
                    FileProvider = new PhysicalFileProvider(
                                                    System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(),
                                                    @Configuration.GetSection("ApiSetting").GetValue<string>("FileServerFolder"))
                                                ),
                    RequestPath = new PathString("/" + Configuration.GetSection("ApiSetting").GetValue<string>("FileServerFolder")),
                    EnableDirectoryBrowsing = true
                });
            }
            //*******End enable file server and file folders mappings*********

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
                                context.Response.AddApplicationError(error.Error.Message);
                                await context.Response.WriteAsync(error.Error.Message).ConfigureAwait(false);
                            }
                        });
                });
        }
    }
}
