using NCARB.EesaService.Entities;

namespace NCARB.EesaService.Infrastructure.Repositories
{
    public class ApplicantRepository : BaseRepository<Applicant>, IApplicantRepository
    {
        public ApplicantRepository(ApplicantContext context)
            : base(context)
        { }
    }
}