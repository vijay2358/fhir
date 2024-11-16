using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace NirvanaHealth.Fhir.Businesses
{
    public interface IBusinessRepository : IRepository<Business, string>
    {
        Task<List<Business>> GetListAsync(
            string filterText = null,           
            int? business_ID = null,
            string businessName = null,
            string businessCode = null,           
            string trackerCode = null,
            string dBA = null,
            string fEIN = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string filterText = null,
            int? business_ID = null,
            string businessName = null,
            string businessCode = null,
            string trackerCode = null,
            string dBA = null,
            string fEIN = null,
            CancellationToken cancellationToken = default);

        Task<Business> GetBusinssByIdAsync(int businessId, CancellationToken cancellationToken = default(CancellationToken));
    }

    
}