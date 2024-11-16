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
using NirvanaHealth.Fhir.MbrEnrollDetails;
using Hl7.Fhir.Model;

namespace NirvanaHealth.Fhir.MbrEnrollDetails
{

    [Authorize(FhirPermissions.MbrEnrollDetails.Default)]
    public class MbrEnrollDetailsAppService : ApplicationService, IMbrEnrollDetailsAppService
    {
        private readonly IMbrEnrollDetailRepository _mbrEnrollDetailRepository;
        private readonly MbrEnrollDetailManager _mbrEnrollDetailManager;

        public MbrEnrollDetailsAppService(IMbrEnrollDetailRepository mbrEnrollDetailRepository, MbrEnrollDetailManager mbrEnrollDetailManager)
        {
            _mbrEnrollDetailRepository = mbrEnrollDetailRepository;
            _mbrEnrollDetailManager = mbrEnrollDetailManager;
        }

        public virtual async Task<PagedResultDto<MbrEnrollDetailDto>> GetListAsync(GetMbrEnrollDetailsInput input)
        {
            var totalCount = await _mbrEnrollDetailRepository.GetCountAsync(input.FilterText, input.MbrEnrollDetail_IDMin, input.MbrEnrollDetail_IDMax, input.MCPMember_IDMin, input.MCPMember_IDMax, input.BenefitPlan_IDMin, input.BenefitPlan_IDMax, input.Member_ID, input.Subscriber_ID, input.PersonCode);
            var items = await _mbrEnrollDetailRepository.GetListAsync(input.FilterText, input.MbrEnrollDetail_IDMin, input.MbrEnrollDetail_IDMax, input.MCPMember_IDMin, input.MCPMember_IDMax, input.BenefitPlan_IDMin, input.BenefitPlan_IDMax, input.Member_ID, input.Subscriber_ID, input.PersonCode, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<MbrEnrollDetailDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<MbrEnrollDetail>, List<MbrEnrollDetailDto>>(items)
            };
        }

        public virtual async Task<Coverage> GetAsync(string id)
        {
            var result = await _mbrEnrollDetailRepository.GetAsync(id);
            if (result == null)
                return null;

            return await _mbrEnrollDetailManager.MapPatientCoverage(result);
        }

        public virtual async Task<Coverage> GetIdAsync(int id)
        {
            var result = await _mbrEnrollDetailRepository.GetMbrEnrollDetailByIdAsync(id);
            if (result == null)
                return null;

            return await _mbrEnrollDetailManager.MapPatientCoverage(result);
        }

        [Authorize(FhirPermissions.MbrEnrollDetails.Delete)]
        public virtual async System.Threading.Tasks.Task DeleteAsync(string id)
        {
            await _mbrEnrollDetailRepository.DeleteAsync(id);
        }

        [Authorize(FhirPermissions.MbrEnrollDetails.Create)]
        public virtual async Task<MbrEnrollDetailDto> CreateAsync(MbrEnrollDetailCreateDto input)
        {

            var mbrEnrollDetail = await _mbrEnrollDetailManager.CreateAsync(
            input.MbrEnrollDetail_ID, input.MCPMember_ID, input.Member_ID, input.Subscriber_ID, input.PersonCode, input.BenefitPlan_ID
            );

            return ObjectMapper.Map<MbrEnrollDetail, MbrEnrollDetailDto>(mbrEnrollDetail);
        }

        [Authorize(FhirPermissions.MbrEnrollDetails.Edit)]
        public virtual async Task<MbrEnrollDetailDto> UpdateAsync(string id, MbrEnrollDetailUpdateDto input)
        {

            var mbrEnrollDetail = await _mbrEnrollDetailManager.UpdateAsync(
            id,
            input.MbrEnrollDetail_ID, input.MCPMember_ID, input.Member_ID, input.Subscriber_ID, input.PersonCode, input.BenefitPlan_ID
            );

            return ObjectMapper.Map<MbrEnrollDetail, MbrEnrollDetailDto>(mbrEnrollDetail);
        }
    }
}