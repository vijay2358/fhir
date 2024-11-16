using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using NirvanaHealth.Fhir.BenefitPlans;
using NirvanaHealth.Fhir.Shared;

namespace NirvanaHealth.Fhir.Web.Pages.Fhir.BenefitPlans
{
    public class IndexModel : AbpPageModel
    {
        public int? BenefitPlan_IDFilterMin { get; set; }

        public int? BenefitPlan_IDFilterMax { get; set; }
        public string BenefitNameFilter { get; set; }
        public string BenefitCodeFilter { get; set; }
        public string DescriptionFilter { get; set; }
        public string VersionNbrFilter { get; set; }

        private readonly IBenefitPlansAppService _benefitPlansAppService;

        public IndexModel(IBenefitPlansAppService benefitPlansAppService)
        {
            _benefitPlansAppService = benefitPlansAppService;
        }

        public async Task OnGetAsync()
        {

            await Task.CompletedTask;
        }
    }
}