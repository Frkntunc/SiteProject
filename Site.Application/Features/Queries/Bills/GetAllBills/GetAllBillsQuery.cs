using MediatR;
using Site.Application.Models.Bill;
using System.Collections.Generic;

namespace Site.Application.Features.Queries.Bills.GetAllBills
{
    public class GetAllBillsQuery:IRequest<List<BillModel>>
    {
    }
}
