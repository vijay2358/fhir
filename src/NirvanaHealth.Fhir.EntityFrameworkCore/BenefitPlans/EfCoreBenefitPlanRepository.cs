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

namespace NirvanaHealth.Fhir.BenefitPlans
{
    public class EfCoreBenefitPlanRepository : EfCoreRepository<FhirDbContext, BenefitPlan, string>, IBenefitPlanRepository
    {
        public EfCoreBenefitPlanRepository(IDbContextProvider<FhirDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<BenefitPlan>> GetListAsync(
            string filterText = null,
            int? benefitPlan_IDMin = null,
            int? benefitPlan_IDMax = null,
            string benefitName = null,
            string benefitCode = null,
            string description = null,
            string versionNbr = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, benefitPlan_IDMin, benefitPlan_IDMax, benefitName, benefitCode, description, versionNbr);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? BenefitPlanConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            int? benefitPlan_IDMin = null,
            int? benefitPlan_IDMax = null,
            string benefitName = null,
            string benefitCode = null,
            string description = null,
            string versionNbr = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, benefitPlan_IDMin, benefitPlan_IDMax, benefitName, benefitCode, description, versionNbr);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<BenefitPlan> ApplyFilter(
            IQueryable<BenefitPlan> query,
            string filterText,
            int? benefitPlan_IDMin = null,
            int? benefitPlan_IDMax = null,
            string benefitName = null,
            string benefitCode = null,
            string description = null,
            string versionNbr = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.BenefitName.Contains(filterText) || e.BenefitCode.Contains(filterText) || e.Description.Contains(filterText) || e.VersionNbr.Contains(filterText))
                    .WhereIf(benefitPlan_IDMin.HasValue, e => e.BenefitPlan_ID >= benefitPlan_IDMin.Value)
                    .WhereIf(benefitPlan_IDMax.HasValue, e => e.BenefitPlan_ID <= benefitPlan_IDMax.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(benefitName), e => e.BenefitName.Contains(benefitName))
                    .WhereIf(!string.IsNullOrWhiteSpace(benefitCode), e => e.BenefitCode.Contains(benefitCode))
                    .WhereIf(!string.IsNullOrWhiteSpace(description), e => e.Description.Contains(description))
                    .WhereIf(!string.IsNullOrWhiteSpace(versionNbr), e => e.VersionNbr.Contains(versionNbr));
        }

        public async Task<BenefitPlan> GetBenefitPlanByIdAsync(int benefitPlanId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = (await GetQueryableAsync())
                    .Where(x => x.BenefitPlan_ID == benefitPlanId);
            return await result.FirstOrDefaultAsync();
        }
    }
}