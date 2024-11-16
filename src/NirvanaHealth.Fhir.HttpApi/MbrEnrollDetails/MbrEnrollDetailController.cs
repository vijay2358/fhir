using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using NirvanaHealth.Fhir.MbrEnrollDetails;

namespace NirvanaHealth.Fhir.MbrEnrollDetails
{
    [RemoteService(Name = "Fhir")]
    [Area("fhir")]
    [ControllerName("MbrEnrollDetail")]
    [Route("api/fhir/mbr-enroll-details")]
    public class MbrEnrollDetailController : FhirController// AbpController, IMbrEnrollDetailsAppService
    {
        private readonly IMbrEnrollDetailsAppService _mbrEnrollDetailsAppService;

        public MbrEnrollDetailController(IMbrEnrollDetailsAppService mbrEnrollDetailsAppService)
        {
            _mbrEnrollDetailsAppService = mbrEnrollDetailsAppService;
        }

        //[HttpGet]
        //public virtual Task<PagedResultDto<MbrEnrollDetailDto>> GetListAsync(GetMbrEnrollDetailsInput input)
        //{
        //    return _mbrEnrollDetailsAppService.GetListAsync(input);
        //}

        [HttpGet]
        [Route("{id}")]
        public virtual async Task<ActionResult> GetAsync(string id)
        {
            return Ok(await FhirResponse(await _mbrEnrollDetailsAppService.GetAsync(id)));
        }

        [HttpGet]
        [Route("/member-enrollmentById/{id}")]
        public virtual async Task<ActionResult> GetByIdAsync(int id)
        {
            return Ok(await FhirResponse(await _mbrEnrollDetailsAppService.GetIdAsync(id)));
        }

        //[HttpPost]
        //public virtual Task<MbrEnrollDetailDto> CreateAsync(MbrEnrollDetailCreateDto input)
        //{
        //    return _mbrEnrollDetailsAppService.CreateAsync(input);
        //}

        //[HttpPut]
        //[Route("{id}")]
        //public virtual Task<MbrEnrollDetailDto> UpdateAsync(string id, MbrEnrollDetailUpdateDto input)
        //{
        //    return _mbrEnrollDetailsAppService.UpdateAsync(id, input);
        //}

        //[HttpDelete]
        //[Route("{id}")]
        //public virtual Task DeleteAsync(string id)
        //{
        //    return _mbrEnrollDetailsAppService.DeleteAsync(id);
        //}
    }
}