using MediatR;

namespace Site.Application.Features.Commands.Apartments.DeleteApartment
{
    public class DeleteApartmentCommand:IRequest
    {
        public DeleteApartmentCommand(int Id)
        {
            ID = Id;
        }
        public int ID { get; set; }
    }
}
