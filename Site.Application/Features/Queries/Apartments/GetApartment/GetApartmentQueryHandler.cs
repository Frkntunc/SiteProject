using AutoMapper;
using MediatR;
using FluentValidation;
using Site.Application.Contracts.Persistence.Repositories.Apartments;
using Site.Application.Models.Apartment;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Site.Domain.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Site.Application.Features.Queries.Apartments.GetApartment
{
    public class GetApartmentQueryHandler : IRequestHandler<GetApartmentQuery, ApartmentModel>
    {
        private readonly IApartmentRepository _apartmentRepository;
        private readonly IMapper _mapper;
        private readonly GetApartmentValidator _validator;
        private readonly IDistributedCache _distributedCache;

        public GetApartmentQueryHandler(IApartmentRepository apartmentRepository, IMapper mapper, IDistributedCache distributedCache)
        {
            _apartmentRepository = apartmentRepository;
            _mapper = mapper;
            _distributedCache = distributedCache;
            _validator = new GetApartmentValidator();
        }

        public async Task<ApartmentModel> Handle(GetApartmentQuery request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request);
            string cacheKey = "GetApartment";
            string json;
            Apartment apartment;
            var apartmentFromCache = await _distributedCache.GetAsync(cacheKey);

            if (apartmentFromCache != null)
            {
                json = Encoding.UTF8.GetString(apartmentFromCache);
                return JsonConvert.DeserializeObject<ApartmentModel>(json);
            }
            else
            {
                apartment = await _apartmentRepository.GetByIdAsync(request.ID);
                json = JsonConvert.SerializeObject(apartment);
                apartmentFromCache = Encoding.UTF8.GetBytes(json);
                var options = new DistributedCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(1))
                    .SetAbsoluteExpiration(DateTime.Now.AddHours(1));
                await _distributedCache.SetAsync(cacheKey, apartmentFromCache, options);
                return _mapper.Map<ApartmentModel>(apartment);
            }
        }
    }
}
