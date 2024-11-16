using NirvanaHealth.Fhir.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using NirvanaHealth.Fhir.BenefitPlans;

namespace NirvanaHealth.Fhir.Web.Pages.Fhir.BenefitPlans
{
    public class EditModalModel : FhirPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }

        [BindProperty]
        public BenefitPlanUpdateDto BenefitPlan { get; set; }

        private readonly IBenefitPlansAppService _benefitPlansAppService;

        public EditModalModel(IBenefitPlansAppService benefitPlansAppService)
        {
            _benefitPlansAppService = benefitPlansAppService;
        }

        public async Task OnGetAsync()
        {
            var benefitPlan = await _benefitPlansAppService.GetAsync(Id);
            BenefitPlan = ObjectMapper.Map<BenefitPlanDto, BenefitPlanUpdateDto>(benefitPlan);

        }

        public async Task<IActionResult> OnPostAsync()
        {

            await _benefitPlansAppService.UpdateAsync(Id, BenefitPlan);
            return NoContent();
        }
    }
}