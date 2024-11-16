using NirvanaHealth.Fhir.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NirvanaHealth.Fhir.BenefitPlans;

namespace NirvanaHealth.Fhir.Web.Pages.Fhir.BenefitPlans
{
    public class CreateModalModel : FhirPageModel
    {
        [BindProperty]
        public BenefitPlanCreateDto BenefitPlan { get; set; }

        private readonly IBenefitPlansAppService _benefitPlansAppService;

        public CreateModalModel(IBenefitPlansAppService benefitPlansAppService)
        {
            _benefitPlansAppService = benefitPlansAppService;
        }

        public async Task OnGetAsync()
        {
            BenefitPlan = new BenefitPlanCreateDto();

            await Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {

            await _benefitPlansAppService.CreateAsync(BenefitPlan);
            return NoContent();
        }
    }
}