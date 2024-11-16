using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using NirvanaHealth.Fhir.MbrEnrollDetails;
using NirvanaHealth.Fhir.Shared;

namespace NirvanaHealth.Fhir.Web.Pages.Fhir.MbrEnrollDetails
{
    public class IndexModel : AbpPageModel
    {
        public int? MbrEnrollDetail_IDFilterMin { get; set; }

        public int? MbrEnrollDetail_IDFilterMax { get; set; }
        public int? MCPMember_IDFilterMin { get; set; }

        public int? MCPMember_IDFilterMax { get; set; }
        public int? BenefitPlan_IDFilterMin { get; set; }

        public int? BenefitPlan_IDFilterMax { get; set; }
        public string Member_IDFilter { get; set; }
        public string Subscriber_IDFilter { get; set; }
        public string PersonCodeFilter { get; set; }

        private readonly IMbrEnrollDetailsAppService _mbrEnrollDetailsAppService;

        public IndexModel(IMbrEnrollDetailsAppService mbrEnrollDetailsAppService)
        {
            _mbrEnrollDetailsAppService = mbrEnrollDetailsAppService;
        }

        public async Task OnGetAsync()
        {

            await Task.CompletedTask;
        }
    }
}