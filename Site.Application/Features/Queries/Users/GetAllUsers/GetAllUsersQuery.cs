using MediatR;
using Site.Application.Models.User;
using System.Collections.Generic;

namespace Site.Application.Features.Queries.Users.GetAllUsers
{
    public class GetAllUsersQuery : IRequest<List<GetUserModel>>
    {
    }
}
