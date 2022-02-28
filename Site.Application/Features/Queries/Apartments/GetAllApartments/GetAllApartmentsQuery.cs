using MediatR;
using Site.Application.Models.Apartment;
using System.Collections.Generic;

namespace Site.Application.Features.Queries.Apartments.GetAllApartments
{
    public class GetAllApartmentsQuery : IRequest<List<ApartmentModel>>
    {
    }
}
