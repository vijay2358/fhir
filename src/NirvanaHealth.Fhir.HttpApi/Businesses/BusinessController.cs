using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using NirvanaHealth.Fhir.Businesses;
using Hl7.Fhir.Model;
using Newtonsoft.Json;
using System.Text;
using Hl7.Fhir.Serialization;

namespace NirvanaHealth.Fhir.Businesses
{
    [RemoteService(Name = "Fhir")]
    [Area("fhir")]
    [ControllerName("Business")]
    [Route("api/fhir/businesses")]
    public class BusinessController :  FhirController
    {
        private readonly IBusinessesAppService _businessesAppService;

        public BusinessController(IBusinessesAppService businessesAppService)
        {
            _businessesAppService = businessesAppService;
        }

        //[HttpGet]
        //public async Task<ActionResult> GetListAsync(GetBusinessesInput input)
        //{

        //    var rest = await _businessesAppService.GetListAsync(input);
        //    StringBuilder sb = new StringBuilder();
        //    var fhirSerializer = new FhirJsonSerializer();
        //    foreach (var item in rest.Items)
        //    {
        //       // var str = await FhirResponse(item);
        //        sb.Append(fhirSerializer.SerializeToString(item));
        //    }
        //    return Ok(sb);
        //}

        [HttpGet]
        [Route("{id}")]
        public virtual async Task<ActionResult> GetAsync(string id)
        {
            return Ok(await FhirResponse(await _businessesAppService.GetAsync(id)));
        }

        [HttpGet]
        [Route("/businessById/{id}")]
        public virtual async Task<ActionResult> GetByIdAsync(int id)
        {
            return Ok(await FhirResponse(await _businessesAppService.GetByIdAsync(id)));
        }

        //[HttpGet]
        //[Route("getBusinssById")]
        //public virtual Task<BusinessDto> GetBusinssByIdAsync(int businessId)
        //{
        //    return _businessesAppService.GetBusinssByIdAsync(businessId);
        //}

        //[HttpPost]
        //public virtual Task<BusinessDto> CreateAsync(BusinessCreateDto input)
        //{
        //    return _businessesAppService.CreateAsync(input);
        //}

        //[HttpPut]
        //[Route("{id}")]
        //public virtual Task<BusinessDto> UpdateAsync(string id, BusinessUpdateDto input)
        //{
        //    return _businessesAppService.UpdateAsync(id, input);
        //}

        //[HttpDelete]
        //[Route("{id}")]
        //public virtual System.Threading.Tasks.Task DeleteAsync(string id)
        //{
        //    return _businessesAppService.DeleteAsync(id);
        //}
    }
}