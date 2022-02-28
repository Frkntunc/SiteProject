using MediatR;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Site.Domain.Authentication;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;

namespace Site.Application.Features.Commands.Users.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly UserManager<User> _userManager;
        private readonly UpdateUserValidator _validator;
        private readonly IDistributedCache _distributedCache;

        public UpdateUserCommandHandler(UserManager<User> userManager,IDistributedCache distributedCache)
        {
            _userManager = userManager;
            _distributedCache = distributedCache;
            _validator = new UpdateUserValidator();
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request);

            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == request.Id);

            if (user == null)
                throw new InvalidOperationException("There is no user with this Id number.");

            user.FirstName = request.FirstName != default ? request.FirstName : user.FirstName;
            user.LastName = request.LastName != default ? request.LastName : user.LastName;
            user.PhoneNumber = request.PhoneNumber != default ? request.PhoneNumber : user.PhoneNumber;
            user.VehicleInformation = request.VehicleInformation != default ? request.VehicleInformation : user.VehicleInformation;

            await _userManager.UpdateAsync(user);
            await _distributedCache.RemoveAsync("GetAllUsers");
            await _distributedCache.RemoveAsync("GetUser");
            return Unit.Value;
        }
    }
}
