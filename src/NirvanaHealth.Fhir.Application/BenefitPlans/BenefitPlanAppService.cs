using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using NirvanaHealth.Fhir.Permissions;
using NirvanaHealth.Fhir.BenefitPlans;
using Hl7.Fhir.Model;
using NirvanaHealth.Fhir.Businesses;

namespace NirvanaHealth.Fhir.BenefitPlans
{

    //[Authorize(FhirPermissions.BenefitPlans.Default)]
    public class BenefitPlansAppService : ApplicationService, IBenefitPlansAppService
    {
        private readonly IBusinessRepository _businessRepository;
        private readonly IBenefitPlanRepository _benefitPlanRepository;
        private readonly BenefitPlanManager _benefitPlanManager;

        public BenefitPlansAppService(IBenefitPlanRepository benefitPlanRepository, IBusinessRepository businessRepository, BenefitPlanManager benefitPlanManager)
        {
            _benefitPlanRepository = benefitPlanRepository;
            _benefitPlanManager = benefitPlanManager;
            _businessRepository = businessRepository;
        }

        public virtual async Task<PagedResultDto<BenefitPlanDto>> GetListAsync(GetBenefitPlansInput input)
        {
            var totalCount = await _benefitPlanRepository.GetCountAsync(input.FilterText, input.BenefitPlan_IDMin, input.BenefitPlan_IDMax, input.BenefitName, input.BenefitCode, input.Description, input.VersionNbr);
            var items = await _benefitPlanRepository.GetListAsync(input.FilterText, input.BenefitPlan_IDMin, input.BenefitPlan_IDMax, input.BenefitName, input.BenefitCode, input.Description, input.VersionNbr, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<BenefitPlanDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<BenefitPlan>, List<BenefitPlanDto>>(items)
            };
        }

        public virtual async Task<Coverage> GetAsync(string id)
        {
            var result = await _benefitPlanRepository.GetAsync(id);
            if (result == null)
                return null;

            var planBusiness = await _businessRepository.GetBusinssByIdAsync(result.Business.Business_ID);
            return await _benefitPlanManager.MapOrganization(result, planBusiness.Id);
        }
        [Authorize(FhirPermissions.BenefitPlans.Delete)]
        public virtual async System.Threading.Tasks.Task DeleteAsync(string id)
        {
            await _benefitPlanRepository.DeleteAsync(id);
        }

        [Authorize(FhirPermissions.BenefitPlans.Create)]
        public virtual async Task<BenefitPlanDto> CreateAsync(BenefitPlanCreateDto input)
        {

            var benefitPlan = await _benefitPlanManager.CreateAsync(
            input.BenefitPlan_ID, input.BenefitName, input.BenefitCode, input.Description, input.VersionNbr
            );

            return ObjectMapper.Map<BenefitPlan, BenefitPlanDto>(benefitPlan);
        }

        [Authorize(FhirPermissions.BenefitPlans.Edit)]
        public virtual async Task<BenefitPlanDto> UpdateAsync(string id, BenefitPlanUpdateDto input)
        {

            var benefitPlan = await _benefitPlanManager.UpdateAsync(
            id,
            input.BenefitPlan_ID, input.BenefitName, input.BenefitCode, input.Description, input.VersionNbr
            );

            return ObjectMapper.Map<BenefitPlan, BenefitPlanDto>(benefitPlan);
        }

        public virtual async Task<Coverage> GetBenefitPlanByIdAsync(int benefitPlanId)
        {
            var result = await _benefitPlanRepository.GetBenefitPlanByIdAsync(benefitPlanId);
            if (result == null)
                return null;

            var planBusiness = await _businessRepository.GetBusinssByIdAsync(result.Business.Business_ID);
            return await _benefitPlanManager.MapOrganization(result, planBusiness.Id);
        }
    }
}