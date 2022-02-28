using AutoMapper;
using MediatR;
using FluentValidation;
using Site.Application.Models.Bill;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Site.Application.Contracts.Persistence.Repositories.BillPayments;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Site.Domain.Dtos;

namespace Site.Application.Features.Queries.Bills.GetBill
{
    public class GetBillQueryHandler:IRequestHandler<GetBillQuery, List<BillModel>>
    {
        private readonly IBillPaymentRepository _billPaymentRepository;
        private readonly IMapper _mapper;
        private readonly GetBillValidator _validator;
        private readonly IDistributedCache _distributedCache;

        public GetBillQueryHandler(IBillPaymentRepository billPaymentRepository, IMapper mapper, IDistributedCache distributedCache)
        {
            _billPaymentRepository = billPaymentRepository;
            _mapper = mapper;
            _distributedCache = distributedCache;
            _validator = new GetBillValidator();
        }

        public async Task<List<BillModel>> Handle(GetBillQuery request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request);

            string cacheKey = "GetBill";
            string json;
            List<BillDto> bills;
            var billFromCache = await _distributedCache.GetAsync(cacheKey);

            if (billFromCache != null)
            {
                json = Encoding.UTF8.GetString(billFromCache);
                return JsonConvert.DeserializeObject<List<BillModel>>(json);
            }
            else
            {
                bills = _billPaymentRepository.GetBillByUserId(request.UserId);
                json = JsonConvert.SerializeObject(bills);
                billFromCache = Encoding.UTF8.GetBytes(json);
                var options = new DistributedCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(1))
                    .SetAbsoluteExpiration(DateTime.Now.AddHours(1));
                await _distributedCache.SetAsync(cacheKey, billFromCache, options);
                return _mapper.Map<List<BillModel>>(bills);
            }
        }
    }
}
