using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Site.Application.Contracts.Persistence.Repositories.Apartments;
using Site.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Site.Application.Features.Commands.Apartments.AddApartment
{
    public class AddApartmentCommandHandler : IRequestHandler<AddApartmentCommand, Apartment>
    {
        private readonly IApartmentRepository _apartmentRepository;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _distributedCache;
        private readonly AddApartmentValidator _validator;

        public AddApartmentCommandHandler(IApartmentRepository apartmentRepository, IMapper mapper,IDistributedCache distributedCache)
        {
            _apartmentRepository = apartmentRepository;
            _distributedCache = distributedCache;
            _mapper = mapper;
            _validator = new AddApartmentValidator();
        }
        public async Task<Apartment> Handle(AddApartmentCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request);

            var apartment = _mapper.Map<Apartment>(request);

            await _apartmentRepository.AddAsync(apartment);

            await _distributedCache.RemoveAsync("GetApartment");
            await _distributedCache.RemoveAsync("GetAllApartments");

            return apartment;
        }
    }
}
