using AutoMapper;
using MediatR;
using FluentValidation;
using Site.Application.Contracts.Persistence.Repositories.Bills;
using System.Threading;
using System.Threading.Tasks;
using Site.Domain.Entities;
using Site.Application.Contracts.Persistence.Repositories.Apartments;
using Site.Application.Contracts.Persistence.Repositories.BillPayments;
using Microsoft.Extensions.Caching.Distributed;

namespace Site.Application.Features.Commands.Bills.AddBill
{
    public class AddBillCommandHandler : IRequestHandler<AddBillCommand>
    {
        private readonly IBillRepository _billRepository;
        private readonly IApartmentRepository _apartmentRepository;
        private readonly IBillPaymentRepository _billPaymentRepository;
        private readonly IDistributedCache _distributedCache;
        private readonly IMapper _mapper;
        private readonly AddBillValidator _validator;
        public AddBillCommandHandler(IBillRepository billRepository, IApartmentRepository apartmentRepository, IBillPaymentRepository billPaymentRepository, IMapper mapper, IDistributedCache distributedCache)
        {
            _billRepository = billRepository;
            _apartmentRepository = apartmentRepository;
            _billPaymentRepository = billPaymentRepository;
            _distributedCache = distributedCache;
            _mapper = mapper;
            _validator = new AddBillValidator();
        }
        public async Task<Unit> Handle(AddBillCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request);

            var addedBill = _mapper.Map<Bill>(request);

            await _billRepository.AddAsync(addedBill);

            //Eklenen faturanın Id'si alındı
            var billId = await _billRepository.GetByIdAsync(addedBill.ID);

            //Bütün daireler çekildi
            var apartments = await _apartmentRepository.GetAllAsync();
            var apartmentFirst = apartments[0];
            var apartmentLast = apartments[apartments.Count - 1];

            //Her daire için daire başına düşen faturalar eklendi 
            for (int i = apartmentFirst.ID; i <= apartmentLast.ID; i++)
            {
                var billPayment = new BillPayment();

                billPayment.BillId = billId.ID;
                billPayment.Electric = addedBill.Electric / apartments.Count;
                billPayment.Water = addedBill.Water / apartments.Count;
                billPayment.NaturalGas = addedBill.NaturalGas / apartments.Count;
                billPayment.Dues = addedBill.Dues;
                billPayment.TotalDept = billPayment.Electric + billPayment.Water + billPayment.NaturalGas + billPayment.Dues;
                billPayment.Month = addedBill.Month;
                var apartmentId = await _apartmentRepository.GetByIdAsync(i);
                billPayment.ApartmentId = apartmentId.ID;

                await _billPaymentRepository.AddAsync(billPayment);
            }

            await _distributedCache.RemoveAsync("GetAllBills");
            await _distributedCache.RemoveAsync("GetBill");
            return Unit.Value;
        }
    }
}
