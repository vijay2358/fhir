using Hl7.Fhir.Model;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace NirvanaHealth.Fhir.MbrEnrollDetails
{
    public interface IMbrEnrollDetailsAppService : IApplicationService
    {
        Task<PagedResultDto<MbrEnrollDetailDto>> GetListAsync(GetMbrEnrollDetailsInput input);

        Task<Coverage> GetAsync(string id);

        Task<Coverage> GetIdAsync(int id);

        System.Threading.Tasks.Task DeleteAsync(string id);

        Task<MbrEnrollDetailDto> CreateAsync(MbrEnrollDetailCreateDto input);

        Task<MbrEnrollDetailDto> UpdateAsync(string id, MbrEnrollDetailUpdateDto input);
    }
}