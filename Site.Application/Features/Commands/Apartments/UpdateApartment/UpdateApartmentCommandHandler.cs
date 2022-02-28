using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Site.Application.Contracts.Persistence.Repositories.Apartments;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Site.Application.Features.Commands.Apartments.UpdateApartment
{
    public class UpdateApartmentCommandHandler : IRequestHandler<UpdateApartmentCommand>
    {
        private readonly IApartmentRepository _apartmentRepository;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _distributedCache;
        private readonly UpdateApartmentValidator _validator;

        public UpdateApartmentCommandHandler(IApartmentRepository apartmentRepository, IMapper mapper, IDistributedCache distributedCache)
        {
            _apartmentRepository = apartmentRepository;
            _mapper = mapper;
            _distributedCache = distributedCache;
            _validator = new UpdateApartmentValidator();
        }

        public async Task<Unit> Handle(UpdateApartmentCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request);

            var apartment = await _apartmentRepository.GetByIdAsync(request.Id);

            if (apartment == null)
                throw new InvalidOperationException("There is no apartment with this id number.");

            apartment.ApartmentNumber = request.ApartmentNumber != default ? request.ApartmentNumber : apartment.ApartmentNumber;
            apartment.Blok = request.Blok != default ? request.Blok : apartment.Blok;
            apartment.Floor = request.Floor != default ? request.Floor : apartment.Floor;
            apartment.Owner = request.Owner != default ? request.Owner : apartment.Owner;
            apartment.Status = request.Status != default ? request.Status : apartment.Status;
            apartment.Type = request.Type != default ? request.Type : apartment.Type;
            apartment.UserId = request.UserId != default ? request.UserId : apartment.UserId;

            await _apartmentRepository.UpdateAsync(apartment);
            await _distributedCache.RemoveAsync("GetApartment");
            await _distributedCache.RemoveAsync("GetAllApartments");

            return Unit.Value;
        }
    }
}
