using System.Collections.Generic;

namespace NCARB.EesaService.Entities
{
    public class Applicant : EntityBase
    {
        public string LastName { get; set; }

        public ICollection<Deficiency> Deficiencies { get; set; }

    }
}