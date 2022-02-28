using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Site.Application.Models.User;
using Site.Domain.Authentication;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Site.Application.Features.Queries.Users.GetAllUsers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<GetUserModel>>
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _distributedCache;

        public GetAllUsersQueryHandler(UserManager<User> userManager, IMapper mapper, IDistributedCache distributedCache)
        {
            _userManager = userManager;
            _mapper = mapper;
            _distributedCache = distributedCache;
        }

        public async Task<List<GetUserModel>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            string cacheKey = "GetAllUsers";
            string json;
            IReadOnlyList<User> users;
            var usersFromCache = await _distributedCache.GetAsync(cacheKey);

            if (usersFromCache != null)
            {
                json = Encoding.UTF8.GetString(usersFromCache);
                return JsonConvert.DeserializeObject<List<GetUserModel>>(json);
            }
            else
            {
                users = await _userManager.Users.ToListAsync();
                json = JsonConvert.SerializeObject(users);
                usersFromCache = Encoding.UTF8.GetBytes(json);
                var options = new DistributedCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(10))
                    .SetAbsoluteExpiration(DateTime.Now.AddHours(1));
                await _distributedCache.SetAsync(cacheKey, usersFromCache, options);
                return _mapper.Map<List<GetUserModel>>(users);
            }
        }
    }
}
