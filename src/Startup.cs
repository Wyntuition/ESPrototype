using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NCARB.EesaService.Infrastructure;
using NCARB.EesaService.Infrastructure.Repositories;

namespace NCARB.EesaService
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
                .AddDbContext<ApplicantContext>(options => options.UseNpgsql("User ID=postgres;Password=password;Server=postgres;Port=5432;Database=EesaService;Integrated Security=true;Pooling=true;"));

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

            // Create DB on startup
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<ApplicantContext>().Database.Migrate();
            }

            // TODO: Not working (saw it working when Seeder was injected)
            // var applicantSeeder = new ApplicantSeeder();
            // applicantSeeder.SeedAsync(app.ApplicationServices).Wait();
        }
    }
}