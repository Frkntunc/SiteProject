using System.ComponentModel.DataAnnotations;

namespace Site.Domain.Entities.Commons
{
    public abstract class EntityBase
    {
        [Key]
        public int ID { get; set; }
    }
}
