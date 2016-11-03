using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using NCARB.EesaService.Entities;

namespace NCARB.EesaService.Infrastructure
{
    public class ApplicantSeeder
    {
        public async Task SeedAsync(IServiceProvider serviceProvider)
        {
            //Based on EF team's example at https://github.com/aspnet/MusicStore/blob/dev/samples/MusicStore/Models/SampleData.cs
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var applicantContext = serviceScope.ServiceProvider.GetService<ApplicantContext>();
                if (await applicantContext.Database.EnsureCreatedAsync())
                {
                    if (!await applicantContext.Applicants.AnyAsync()) {
                      await InsertData(applicantContext);
                    }
                }
            }
        }

        public async Task InsertData(ApplicantContext db)
        {
            var applicant = new Applicant { 
                        Id = new Random().Next(),
                        LastName = "LastName",
                    };

            db.Applicants.Add(applicant);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception exp)
            {                
                throw; 
            }
        }

    }
}