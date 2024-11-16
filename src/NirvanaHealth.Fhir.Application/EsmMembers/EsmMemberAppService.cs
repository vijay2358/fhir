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
using NirvanaHealth.Fhir.EsmMembers;
using Hl7.Fhir.Model;
using Microsoft.Extensions.Configuration;

namespace NirvanaHealth.Fhir.EsmMembers
{

    [Authorize(FhirPermissions.EsmMembers.Default)]
    public class EsmMembersAppService : ApplicationService, IEsmMembersAppService
    {
        private readonly IEsmMemberRepository _esmMemberRepository;
        private readonly EsmMemberManager _esmMemberManager;
        private readonly IConfiguration _configuration;

        public EsmMembersAppService(IEsmMemberRepository esmMemberRepository, EsmMemberManager esmMemberManager, IConfiguration configuration)
        {
            _esmMemberRepository = esmMemberRepository;
            _esmMemberManager = esmMemberManager;
            _configuration = configuration;

        }

        public virtual async Task<PagedResultDto<EsmMemberDto>> GetListAsync(GetEsmMembersInput input)
        {
            var totalCount = await _esmMemberRepository.GetCountAsync(input.FilterText, input.MCPMember_IDMin, input.MCPMember_IDMax, input.LastName, input.FirstName, input.MIddleName, input.Suffix, input.PreFix, input.DOBMin, input.DOBMax, input.Gender, input.DateofDeathMin, input.DateofDeathMax, input.Race);
            var items = await _esmMemberRepository.GetListAsync(input.FilterText, input.MCPMember_IDMin, input.MCPMember_IDMax, input.LastName, input.FirstName, input.MIddleName, input.Suffix, input.PreFix, input.DOBMin, input.DOBMax, input.Gender, input.DateofDeathMin, input.DateofDeathMax, input.Race, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<EsmMemberDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<EsmMember>, List<EsmMemberDto>>(items)
            };
        }

        public virtual async Task<Patient> GetAsync(string id)
        {
            var result = await _esmMemberRepository.GetAsync(id);
            if (result == null)
                return null;

            return await _esmMemberManager.MapPatient(result);
        }

        public virtual async Task<Patient> GetByIdAsync(int id)
        {
            var result = await _esmMemberRepository.GetMemberByIdAsync(id);
            if (result == null)
                return null;

            return await _esmMemberManager.MapPatient(result);
        }


        [Authorize(FhirPermissions.EsmMembers.Delete)]
        public virtual async System.Threading.Tasks.Task DeleteAsync(string id)
        {
            await _esmMemberRepository.DeleteAsync(id);
        }

        [Authorize(FhirPermissions.EsmMembers.Create)]
        public virtual async Task<EsmMemberDto> CreateAsync(EsmMemberCreateDto input)
        {

            var esmMember = await _esmMemberManager.CreateAsync(
            input.MCPMember_ID, input.LastName, input.FirstName, input.MIddleName, input.Suffix, input.PreFix, input.Gender, input.DOB, input.DateofDeath, input.Race
            );

            return ObjectMapper.Map<EsmMember, EsmMemberDto>(esmMember);
        }

        [Authorize(FhirPermissions.EsmMembers.Edit)]
        public virtual async Task<EsmMemberDto> UpdateAsync(string id, EsmMemberUpdateDto input)
        {

            var esmMember = await _esmMemberManager.UpdateAsync(
            id,
            input.MCPMember_ID, input.LastName, input.FirstName, input.MIddleName, input.Suffix, input.PreFix, input.Gender, input.DOB, input.DateofDeath, input.Race
            );

            return ObjectMapper.Map<EsmMember, EsmMemberDto>(esmMember);
        }
    }
}