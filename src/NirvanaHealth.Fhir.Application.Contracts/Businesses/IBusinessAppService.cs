using Hl7.Fhir.Model;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace NirvanaHealth.Fhir.Businesses
{
    public interface IBusinessesAppService : IApplicationService
    {
        Task<PagedResultDto<Organization>> GetListAsync(GetBusinessesInput input);

        //Task<BusinessDto> GetAsync(string id);
        Task<Organization> GetAsync(string id);

        Task<Organization> GetByIdAsync(int id);

        System.Threading.Tasks.Task DeleteAsync(string id);

        Task<BusinessDto> CreateAsync(BusinessCreateDto input);

        Task<BusinessDto> UpdateAsync(string id, BusinessUpdateDto input);

    }
}