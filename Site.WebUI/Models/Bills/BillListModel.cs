namespace Site.WebUI.Models.Bills
{
    public class BillListModel
    {
        public decimal Electric { get; set; }
        public decimal Water { get; set; }
        public decimal NaturalGas { get; set; }
        public decimal Dues { get; set; }
        public string Month { get; set; }
        public decimal TotalDept { get; set; }
    }
}
