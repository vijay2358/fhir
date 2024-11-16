using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace NirvanaHealth.Fhir.BenefitPlans
{
    public interface IBenefitPlanRepository : IRepository<BenefitPlan, string>
    {
        Task<List<BenefitPlan>> GetListAsync(
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
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string filterText = null,
            int? benefitPlan_IDMin = null,
            int? benefitPlan_IDMax = null,
            string benefitName = null,
            string benefitCode = null,
            string description = null,
            string versionNbr = null,
            CancellationToken cancellationToken = default);

        Task<BenefitPlan> GetBenefitPlanByIdAsync(int benefitPlanId, CancellationToken cancellationToken = default(CancellationToken));
    }
}