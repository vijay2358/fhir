//using System;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Volo.Abp;
//using Volo.Abp.AspNetCore.Mvc;
//using Volo.Abp.Application.Dtos;
//using NirvanaHealth.Fhir.BenefitPlans;
//using Hl7.Fhir.Model;
//using System.Text;
//using Hl7.Fhir.Serialization;

//namespace NirvanaHealth.Fhir.BenefitPlans
//{
//    [RemoteService(Name = "Fhir")]
//    [Area("fhir")]
//    [ControllerName("BenefitPlan")]
//    [Route("api/fhir/benefit-plans")]
//    public class BenefitPlanController : FhirController//  AbpController, IBenefitPlansAppService
//    {
//        private readonly IBenefitPlansAppService _benefitPlansAppService;

//        public BenefitPlanController(IBenefitPlansAppService benefitPlansAppService)
//        {
//            _benefitPlansAppService = benefitPlansAppService;
//        }

//        [HttpGet]
//        public virtual async Task<ActionResult> GetListAsync(GetBenefitPlansInput input)
//        {
//            //var rest = await _benefitPlansAppService.GetListAsync(input);
//            //StringBuilder sb = new StringBuilder();
//            //var fhirSerializer = new FhirJsonSerializer();
//            //foreach (var item in rest.Items)
//            //{
//            //    // var str = await FhirResponse(item);
//            //    sb.Append(fhirSerializer.SerializeToString(item));
//            //}
//            return Ok();
//        }

//        [HttpGet]
//        [Route("{id}")]
//        public virtual async Task<ActionResult> GetAsync(string id)
//        {
//            return Ok(await FhirResponse(await _benefitPlansAppService.GetAsync(id)));
//        }

//        [HttpGet]
//        [Route("getBenefitPlanById")]
//        public virtual async Task<ActionResult> GetBenefitPlanByIdAsync(int benefitPlanId)
//        {
//            return Ok(await FhirResponse(await _benefitPlansAppService.GetBenefitPlanByIdAsync(benefitPlanId)));
//        }

//        //[HttpPost]
//        //public virtual Task<BenefitPlanDto> CreateAsync(BenefitPlanCreateDto input)
//        //{
//        //    return _benefitPlansAppService.CreateAsync(input);
//        //}

//        //[HttpPut]
//        //[Route("{id}")]
//        //public virtual Task<BenefitPlanDto> UpdateAsync(string id, BenefitPlanUpdateDto input)
//        //{
//        //    return _benefitPlansAppService.UpdateAsync(id, input);
//        //}

//        //[HttpDelete]
//        //[Route("{id}")]
//        //public virtual System.Threading.Tasks.Task DeleteAsync(string id)
//        //{
//        //    return _benefitPlansAppService.DeleteAsync(id);
//        //}
//    }
//}