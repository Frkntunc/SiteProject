using MediatR;
using Site.Application.Models.User;

namespace Site.Application.Features.Queries.Users.GetUser
{
    public class GetUserQuery : IRequest<GetUserModel>
    {
        public GetUserQuery(int Id)
        {
            ID = Id;
        }
        public int ID { get; set; }
    }
}
