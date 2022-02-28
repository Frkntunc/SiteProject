using FluentValidation;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Site.Application.Contracts.Persistence.Repositories.Apartments;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Site.Application.Features.Commands.Apartments.DeleteApartment
{
    public class DeleteApartmentCommandHandler : IRequestHandler<DeleteApartmentCommand>
    {
        private readonly IApartmentRepository _apartmentRepository;
        private readonly IDistributedCache _distributedCache;
        private readonly DeleteApartmentValidator _validator;

        public DeleteApartmentCommandHandler(IApartmentRepository apartmentRepository,IDistributedCache distributedCache)
        {
            _apartmentRepository = apartmentRepository;
            _distributedCache = distributedCache;
            _validator = new DeleteApartmentValidator();
        }

        public async Task<Unit> Handle(DeleteApartmentCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request);
            var apartment = await _apartmentRepository.GetByIdAsync(request.ID);

            if (apartment==null)
                throw new InvalidOperationException("There is no apartment with this id number.");

            await _apartmentRepository.RemoveAsync(apartment);
            await _distributedCache.RemoveAsync("GetApartment");
            await _distributedCache.RemoveAsync("GetAllApartments");

            return Unit.Value;
        }
    }
}
