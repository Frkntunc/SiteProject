using Site.Domain.Entities.Commons;

namespace Site.Domain.Entities
{
    public class BillPayment : EntityBase
    {
        public Bill Bill { get; set; }
        public int BillId { get; set; }
        public Apartment Apartment { get; set; }
        public int ApartmentId { get; set; }
        public decimal Electric { get; set; }
        public decimal Water { get; set; }
        public decimal NaturalGas { get; set; }
        public decimal Dues { get; set; }
        public decimal TotalDept { get; set; }
        public string Month { get; set; }
    }
}
