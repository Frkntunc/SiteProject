using MediatR;
using Site.Application.Models.Apartment;

namespace Site.Application.Features.Queries.Apartments.GetApartment
{
    public class GetApartmentQuery:IRequest<ApartmentModel>
    {
        public GetApartmentQuery(int Id)
        {
            ID = Id;
        }

        public int ID { get; set; }
    }
}
