using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;
using Site.Domain.Authentication;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Site.Application.Features.Commands.Users.AddUser
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, string>
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly AddUserValidator _validator;
        private readonly IDistributedCache _distributedCache;
        Random _rnd;

        public AddUserCommandHandler(UserManager<User> userManager, IMapper mapper, IDistributedCache distributedCache)
        {
            _userManager = userManager;
            _mapper = mapper;
            _distributedCache = distributedCache;
            _validator = new AddUserValidator();
            _rnd = new Random();
        }
        public async Task<string> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request);

            var controlUser = await _userManager.FindByEmailAsync(request.Email);

            if (controlUser != null)
                throw new InvalidOperationException("E-mail is already exist.");

            var user = _mapper.Map<User>(request);

            var pin = _rnd.Next(1000, 9999).ToString();

            user.UserName = request.Email;
            var createdUser = await _userManager.CreateAsync(user,pin);

            if (createdUser.Succeeded)
            {
                await _distributedCache.RemoveAsync("GetAllUsers");
                await _distributedCache.RemoveAsync("GetUser");
                return pin;
            }

            return null;
        }
    }
}
