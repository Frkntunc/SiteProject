using AutoMapper;
using PaymentService.Application.Features.Commands.CreditCards.AddCreditCard;
using PaymentService.Domain.Entities;

namespace PaymentService.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreditCard, AddCreditCardCommand>().ReverseMap();
        }
    }
}
