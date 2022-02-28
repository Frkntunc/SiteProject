using MediatR;

namespace Site.Application.Features.Commands.Bills.AddBill
{
    public class AddBillCommand:IRequest
    {
        public decimal Electric { get; set; }
        public decimal Water { get; set; }
        public decimal NaturalGas { get; set; }
        public decimal Dues { get; set; }
        public int Month { get; set; }
    }
}
