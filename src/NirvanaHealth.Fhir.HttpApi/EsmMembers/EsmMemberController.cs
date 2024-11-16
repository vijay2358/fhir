using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using NirvanaHealth.Fhir.EsmMembers;
using System.Text;
using Hl7.Fhir.Serialization;

namespace NirvanaHealth.Fhir.EsmMembers
{
    [RemoteService(Name = "Fhir")]
    [Area("fhir")]
    [ControllerName("Member")]
    [Route("api/fhir/members")]
    public class EsmMemberController : FhirController// AbpController, IEsmMembersAppService
    {
        private readonly IEsmMembersAppService _esmMembersAppService;

        public EsmMemberController(IEsmMembersAppService esmMembersAppService)
        {
            _esmMembersAppService = esmMembersAppService;
        }

        //[HttpGet]
        //public virtual async Task<ActionResult> GetListAsync(GetEsmMembersInput input)
        //{
        //    //var rest = await _esmMembersAppService.GetListAsync(input);
        //    //StringBuilder sb = new StringBuilder();
        //    //var fhirSerializer = new FhirJsonSerializer();
        //    //foreach (var item in rest.Items)
        //    //{
        //    //    // var str = await FhirResponse(item);
        //    //    sb.Append(fhirSerializer.SerializeToString(item));
        //    //}
        //    return Ok();
        //}

        [HttpGet]
        [Route("{id}")]
        public virtual async Task<ActionResult> GetAsync(string id)
        {
            return Ok(await FhirResponse(await _esmMembersAppService.GetAsync(id)));
        }

        [HttpGet]
        [Route("/memberById/{id}")]
        public virtual async Task<ActionResult> GetByIdAsync(int id)
        {
            return Ok(await FhirResponse(await _esmMembersAppService.GetByIdAsync(id)));
        }

        //[HttpPost]
        //public virtual Task<EsmMemberDto> CreateAsync(EsmMemberCreateDto input)
        //{
        //    return _esmMembersAppService.CreateAsync(input);
        //}

        //[HttpPut]
        //[Route("{id}")]
        //public virtual Task<EsmMemberDto> UpdateAsync(string id, EsmMemberUpdateDto input)
        //{
        //    return _esmMembersAppService.UpdateAsync(id, input);
        //}

        //[HttpDelete]
        //[Route("{id}")]
        //public virtual Task DeleteAsync(string id)
        //{
        //    return _esmMembersAppService.DeleteAsync(id);
        //}
    }
}