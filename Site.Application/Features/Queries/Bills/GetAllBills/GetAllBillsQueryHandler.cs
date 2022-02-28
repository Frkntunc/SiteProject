using AutoMapper;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Site.Application.Contracts.Persistence.Repositories.Bills;
using Site.Application.Models.Bill;
using Site.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Site.Application.Features.Queries.Bills.GetAllBills
{
    public class GetAllBillsQueryHandler : IRequestHandler<GetAllBillsQuery, List<BillModel>>
    {
        private readonly IBillRepository _billRepository;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _distributedCache;

        public GetAllBillsQueryHandler(IBillRepository billRepository, IMapper mapper, IDistributedCache distributedCache)
        {
            _billRepository = billRepository;
            _mapper = mapper;
            _distributedCache = distributedCache;
        }

        public async Task<List<BillModel>> Handle(GetAllBillsQuery request, CancellationToken cancellationToken)
        {
            string cacheKey = "GetAllBills";

            string json;
            IReadOnlyList<Bill> bills;
            var billsFromCache = await _distributedCache.GetAsync(cacheKey);

            if (billsFromCache != null)
            {
                json = Encoding.UTF8.GetString(billsFromCache);
                return JsonConvert.DeserializeObject<List<BillModel>>(json);
            }
            else
            {
                bills = await _billRepository.GetAllAsync();
                json = JsonConvert.SerializeObject(bills);
                billsFromCache = Encoding.UTF8.GetBytes(json);
                var options = new DistributedCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(10))
                    .SetAbsoluteExpiration(DateTime.Now.AddHours(1));
                await _distributedCache.SetAsync(cacheKey, billsFromCache, options);
                return _mapper.Map<List<BillModel>>(bills);
            }
        }
    }
}
