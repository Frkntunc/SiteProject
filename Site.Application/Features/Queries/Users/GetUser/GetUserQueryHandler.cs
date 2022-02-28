using AutoMapper;
using MediatR;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Site.Application.Models.User;
using Site.Domain.Authentication;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Site.Application.Features.Queries.Users.GetUser
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, GetUserModel>
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly GetUserValidator _validator;
        private readonly IDistributedCache _distributedCache;

        public GetUserQueryHandler(UserManager<User> userManager, IMapper mapper, IDistributedCache distributedCache)
        {
            _userManager = userManager;
            _mapper = mapper;
            _validator = new GetUserValidator();
            _distributedCache = distributedCache;
        }

        public async Task<GetUserModel> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request);

            string cacheKey = "GetUser";
            string json;
            User user;
            var userFromCache = await _distributedCache.GetAsync(cacheKey);

            if (userFromCache != null)
            {
                json = Encoding.UTF8.GetString(userFromCache);
                return JsonConvert.DeserializeObject<GetUserModel>(json);
            }
            else
            {
                user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == request.ID);
                json = JsonConvert.SerializeObject(user);
                userFromCache = Encoding.UTF8.GetBytes(json);
                var options = new DistributedCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(1))
                    .SetAbsoluteExpiration(DateTime.Now.AddHours(1));
                await _distributedCache.SetAsync(cacheKey, userFromCache, options);
                return _mapper.Map<GetUserModel>(user);
            }
        }
    }
}
