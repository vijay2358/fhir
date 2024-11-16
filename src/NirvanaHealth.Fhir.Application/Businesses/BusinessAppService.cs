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
using NirvanaHealth.Fhir.Businesses;
using Hl7.Fhir.Model;

namespace NirvanaHealth.Fhir.Businesses
{

    //[Authorize(FhirPermissions.Businesses.Default)]
    public class BusinessesAppService : ApplicationService, IBusinessesAppService
    {
        private readonly IBusinessRepository _businessRepository;
        private readonly BusinessManager _businessManager;

        public BusinessesAppService(IBusinessRepository businessRepository, BusinessManager businessManager)
        {
            _businessRepository = businessRepository;
            _businessManager = businessManager;
        }

        public virtual async Task<PagedResultDto<Organization>> GetListAsync(GetBusinessesInput input)
        {
            var totalCount = await _businessRepository.GetCountAsync(input.FilterText, input.Business_ID, input.BusinessName, input.BusinessCode, input.TrackerCode, input.DBA, input.FEIN);
            var items = await _businessRepository.GetListAsync(input.FilterText, input.Business_ID , input.BusinessName, input.BusinessCode,  input.TrackerCode, input.DBA, input.FEIN, input.Sorting, input.MaxResultCount, input.SkipCount);

            var listPayers = new List<Organization>();
            foreach (var item in items)
            {
                listPayers.Add(await _businessManager.MapOrganization(item));
            }

            return new PagedResultDto<Organization>
            {
                TotalCount = totalCount,
                Items = listPayers
            };
        }

        public virtual async Task<Organization> GetAsync(string id)
        {
            var result = await _businessRepository.GetAsync(id);
            if (result == null)
                return null;

            return await _businessManager.MapOrganization(result);
        }

        public virtual async Task<Organization> GetByIdAsync(int businessId)
        {
            var result = await _businessRepository.GetBusinssByIdAsync(businessId);
            if (result == null)
                return null;

            return await _businessManager.MapOrganization(result);
        }

        [Authorize(FhirPermissions.Businesses.Delete)]
        public virtual async System.Threading.Tasks.Task DeleteAsync(string id)
        {
            await _businessRepository.DeleteAsync(id);
        }

        [Authorize(FhirPermissions.Businesses.Create)]
        public virtual async Task<BusinessDto> CreateAsync(BusinessCreateDto input)
        {

            var business = await _businessManager.CreateAsync(
            input.Business_ID, input.BusinessName, input.BusinessCode, input.TrackerCode, input.DBA, input.FEIN, input.BusinessCat
            );

            return ObjectMapper.Map<Business, BusinessDto>(business);
        }

        [Authorize(FhirPermissions.Businesses.Edit)]
        public virtual async Task<BusinessDto> UpdateAsync(string id, BusinessUpdateDto input)
        {

            var business = await _businessManager.UpdateAsync(
            id,
            input.Business_ID, input.BusinessName, input.BusinessCode, input.TrackerCode, input.DBA, input.FEIN, input.BusinessCat
            );

            return ObjectMapper.Map<Business, BusinessDto>(business);
        }
    }
}