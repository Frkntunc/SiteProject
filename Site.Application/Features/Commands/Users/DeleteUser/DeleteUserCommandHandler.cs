using MediatR;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Site.Domain.Authentication;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;

namespace Site.Application.Features.Commands.Users.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly UserManager<User> _userManager;
        private readonly DeleteUserValidator _validator;
        private readonly IDistributedCache _distributedCache;

        public DeleteUserCommandHandler(UserManager<User> userManager,IDistributedCache distributedCache)
        {
            _userManager = userManager;
            _distributedCache = distributedCache;
            _validator = new DeleteUserValidator();
        }

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request);

            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == request.ID);

            if (user==null)
                throw new InvalidOperationException("There is no user with this Id number.");

            await _userManager.DeleteAsync(user);
            await _distributedCache.RemoveAsync("GetAllUsers");
            await _distributedCache.RemoveAsync("GetUser");
            return Unit.Value;
        }
    }
}
