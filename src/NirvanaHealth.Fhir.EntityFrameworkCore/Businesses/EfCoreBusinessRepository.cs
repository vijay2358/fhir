using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using NirvanaHealth.Fhir.EntityFrameworkCore;

namespace NirvanaHealth.Fhir.Businesses
{
    public class EfCoreBusinessRepository : EfCoreRepository<FhirDbContext, Business, string>, IBusinessRepository
    {
        public EfCoreBusinessRepository(IDbContextProvider<FhirDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<Business>> GetListAsync(
            string filterText = null,
            int? business_ID= null,
            string businessName = null,
            string businessCode = null,
            string trackerCode = null,
            string dBA = null,
            string fEIN = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, business_ID, businessName, businessCode, trackerCode, dBA, fEIN);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? BusinessConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            int? business_ID = null,
            string businessName = null,
            string businessCode = null,
            string trackerCode = null,
            string dBA = null,
            string fEIN = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, business_ID, businessName, businessCode, trackerCode, dBA, fEIN);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Business> ApplyFilter(
            IQueryable<Business> query,
            string filterText,
            int? business_ID = null,
            string businessName = null,
            string businessCode = null,
            string trackerCode = null,
            string dBA = null,
            string fEIN = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.BusinessName.Contains(filterText) || e.BusinessCode.Contains(filterText) || e.TrackerCode.Contains(filterText) || e.DBA.Contains(filterText) || e.FEIN.Contains(filterText))
                    .WhereIf(business_ID.HasValue, e => e.Business_ID <= business_ID.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(businessName), e => e.BusinessName.Contains(businessName))
                    .WhereIf(!string.IsNullOrWhiteSpace(businessCode), e => e.BusinessCode.Contains(businessCode))
                    .WhereIf(!string.IsNullOrWhiteSpace(trackerCode), e => e.TrackerCode.Contains(trackerCode))
                    .WhereIf(!string.IsNullOrWhiteSpace(dBA), e => e.DBA.Contains(dBA))
                    .WhereIf(!string.IsNullOrWhiteSpace(fEIN), e => e.FEIN.Contains(fEIN));
        }

        public async Task<Business> GetBusinssByIdAsync(int businessId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = (await GetQueryableAsync())
                    .Where(x => x.Business_ID == businessId);
            return await result.FirstOrDefaultAsync();
        }
    }
}