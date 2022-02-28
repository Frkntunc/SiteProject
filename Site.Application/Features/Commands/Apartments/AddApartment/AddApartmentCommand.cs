using MediatR;
using Site.Domain.Entities;

namespace Site.Application.Features.Commands.Apartments.AddApartment
{
    public class AddApartmentCommand:IRequest<Apartment>
    {
        public byte Blok { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public byte Floor { get; set; }
        public byte ApartmentNumber { get; set; }
        public string Owner { get; set; }
        public int UserId { get; set; }
    }
}
