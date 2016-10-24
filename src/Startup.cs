using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NCARB.EesaService.Infrastructure;
using NCARB.EesaService.Infrastructure.Repositories;

namespace ConsoleApplication
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder() // Collection of sources for read/write key/value pairs
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables(); // Overrides environment variables with valiues from config files/etc
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore()
                .AddJsonFormatters();

            // services.AddDbContext<ApplicantContext>(options =>
            // {
            //     options.UseSqlite(Configuration.GetConnectionString("EesaService"));
            // });
            // Add PostgreSQL support. NOTE, had to run -sf commands at the end of this thread to fix openssl, https://github.com/dotnet/cli/issues/3783
            services.AddEntityFrameworkNpgsql()
                .AddDbContext<ApplicantContext>(options => options.UseNpgsql(Configuration.GetConnectionString("EesaServiceConnectionString")));

            services.AddScoped<IApplicantRepository, ApplicantRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory, IHostingEnvironment env)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            var startupLogger = loggerFactory.CreateLogger<Startup>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            startupLogger.LogTrace("Trace test output!");
            startupLogger.LogDebug("Debug test output!");
            startupLogger.LogInformation("Info test output!");
            startupLogger.LogError("Error test output!");
            startupLogger.LogCritical("Trace test output!");
        }
    }
}