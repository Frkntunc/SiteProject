using Site.Domain.Entities.Commons;

namespace Site.Domain.Entities
{
    public class Bill:EntityBase
    {
        public decimal Electric { get; set; }
        public decimal Water { get; set; }
        public decimal NaturalGas { get; set; }
        public decimal Dues { get; set; }
        public decimal TotalDept { get; set; }
        public string Month { get; set; }
    }
}
