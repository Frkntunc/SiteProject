using AutoMapper;
using Site.Application.Features.Commands.Apartments.AddApartment;
using Site.Application.Features.Commands.Bills.AddBill;
using Site.Application.Features.Commands.Messages.SendMessage;
using Site.Application.Features.Commands.Users.AddUser;
using Site.Application.Models.Apartment;
using Site.Application.Models.Authentication;
using Site.Application.Models.Bill;
using Site.Application.Models.Message;
using Site.Application.Models.User;
using Site.Domain.Authentication;
using Site.Domain.Dtos;
using Site.Domain.Entities;
using Site.Domain.Enums;

namespace Site.Application.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserModel>().ReverseMap();
            CreateMap<User, AddUserCommand>().ReverseMap();
            CreateMap<GetUserModel, User>().ReverseMap();

            CreateMap<Apartment, AddApartmentCommand>().ReverseMap();
            CreateMap<ApartmentModel, Apartment>().ReverseMap();

            CreateMap<Bill, AddBillCommand>().ReverseMap().ForMember(dest => dest.Month, opt => opt.MapFrom(src => ((MonthEnum)src.Month).ToString()));
            CreateMap<BillDto, BillModel>().ReverseMap();

            CreateMap<Message, SendMessageCommand>().ReverseMap();
            CreateMap<MessageModel, MessageDto>().ReverseMap().ForMember(dest => dest.Read, opt => opt.MapFrom(src => src.Read == false ? "Yeni Mesaj" : "Okunmuş"));
        }
    }
}
