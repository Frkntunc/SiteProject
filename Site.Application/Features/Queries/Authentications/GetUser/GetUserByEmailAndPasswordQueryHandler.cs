using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Site.Application.Models.Authentication;
using Site.Domain.Authentication;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Site.Application.Features.Queries.Authentications.GetUser
{
    public class GetUserByEmailAndPasswordQueryHandler : IRequestHandler<GetUserByEmailAndPasswordQuery, UserModel>
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public GetUserByEmailAndPasswordQueryHandler(UserManager<User> userManager,IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<UserModel> Handle(GetUserByEmailAndPasswordQuery request, CancellationToken cancellationToken)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Email == request.Email);

            if (user == null)
                throw new InvalidOperationException("There is no user with this email.");

            var userModel = new UserModel();

            var userSigingResult = await _userManager.CheckPasswordAsync(user, request.Password);

            if (userSigingResult)
            {
                userModel = _mapper.Map<UserModel>(user);
                userModel.Roles = await _userManager.GetRolesAsync(user);
            }

            return userModel;
        }
    }
}
