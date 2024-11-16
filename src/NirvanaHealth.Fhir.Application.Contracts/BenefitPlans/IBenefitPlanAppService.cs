using Hl7.Fhir.Model;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace NirvanaHealth.Fhir.BenefitPlans
{
    public interface IBenefitPlansAppService : IApplicationService
    {
        Task<PagedResultDto<BenefitPlanDto>> GetListAsync(GetBenefitPlansInput input);

        Task<Coverage> GetAsync(string id);

        System.Threading.Tasks.Task DeleteAsync(string id);

        Task<BenefitPlanDto> CreateAsync(BenefitPlanCreateDto input);

        Task<BenefitPlanDto> UpdateAsync(string id, BenefitPlanUpdateDto input);

        Task<Coverage> GetBenefitPlanByIdAsync(int benefitPlanId);
    }
}