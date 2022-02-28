using MediatR;
using Site.Application.Models.Bill;
using System.Collections.Generic;

namespace Site.Application.Features.Queries.Bills.GetBill
{
    public class GetBillQuery:IRequest<List<BillModel>>
    {
        public GetBillQuery(int userId)
        {
            UserId = userId;
        }

        public int UserId { get; set; }
    }
}
