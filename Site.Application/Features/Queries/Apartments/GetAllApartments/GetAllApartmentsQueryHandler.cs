using AutoMapper;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Site.Application.Contracts.Persistence.Repositories.Apartments;
using Site.Application.Models.Apartment;
using Site.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Site.Application.Features.Queries.Apartments.GetAllApartments
{
    public class GetAllApartmentsQueryHandler : IRequestHandler<GetAllApartmentsQuery, List<ApartmentModel>>
    {
        private readonly IApartmentRepository _apartmentRepository;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _distributedCache;

        public GetAllApartmentsQueryHandler(IApartmentRepository apartmentRepository, IMapper mapper, IDistributedCache distributedCache)
        {
            _apartmentRepository = apartmentRepository;
            _mapper = mapper;
            _distributedCache = distributedCache;
        }

        public async Task<List<ApartmentModel>> Handle(GetAllApartmentsQuery request, CancellationToken cancellationToken)
        {
            string cacheKey = "GetAllApartments";
            string json;
            IReadOnlyList<Apartment> apartments;
            var apartmentsFromCache = await _distributedCache.GetAsync(cacheKey);

            if (apartmentsFromCache != null)
            {
                json = Encoding.UTF8.GetString(apartmentsFromCache);
                return JsonConvert.DeserializeObject<List<ApartmentModel>>(json);
            }
            else
            {
                apartments = await _apartmentRepository.GetAllAsync();
                json = JsonConvert.SerializeObject(apartments);
                apartmentsFromCache = Encoding.UTF8.GetBytes(json);
                var options = new DistributedCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(10))
                    .SetAbsoluteExpiration(DateTime.Now.AddHours(1));
                await _distributedCache.SetAsync(cacheKey, apartmentsFromCache, options);
                return _mapper.Map<List<ApartmentModel>>(apartments);
            }
        }
    }
}
