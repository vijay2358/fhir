using Hl7.Fhir.Model;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace NirvanaHealth.Fhir.EsmMembers
{
    public interface IEsmMembersAppService : IApplicationService
    {
        Task<PagedResultDto<EsmMemberDto>> GetListAsync(GetEsmMembersInput input);

        Task<Patient> GetAsync(string id);

        Task<Patient> GetByIdAsync(int id);

        System.Threading.Tasks.Task DeleteAsync(string id);

        Task<EsmMemberDto> CreateAsync(EsmMemberCreateDto input);

        Task<EsmMemberDto> UpdateAsync(string id, EsmMemberUpdateDto input);
    }
}