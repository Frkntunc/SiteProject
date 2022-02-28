namespace Site.WebUI.Models.Bills
{
    public class AddBillModel
    {
        public decimal Electric { get; set; }
        public decimal Water { get; set; }
        public decimal NaturalGas { get; set; }
        public decimal Dues { get; set; }
        public int Month { get; set; }
    }
}
