using System.ComponentModel.DataAnnotations.Schema;

namespace NCARB.EesaService.Entities
{
    public class EntityBase : IEntityBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
    }
}
