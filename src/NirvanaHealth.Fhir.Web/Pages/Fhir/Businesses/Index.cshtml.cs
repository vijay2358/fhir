using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using NirvanaHealth.Fhir.Businesses;
using NirvanaHealth.Fhir.Shared;

namespace NirvanaHealth.Fhir.Web.Pages.Fhir.Businesses
{
    public class IndexModel : AbpPageModel
    {
        public int? Business_IDFilterMin { get; set; }

        public int? Business_IDFilterMax { get; set; }
        public string BusinessNameFilter { get; set; }
        public string BusinessCodeFilter { get; set; }
        public int? BusinessCatFilterMin { get; set; }

        public int? BusinessCatFilterMax { get; set; }
        public string TrackerCodeFilter { get; set; }
        public string DBAFilter { get; set; }
        public string FEINFilter { get; set; }

        private readonly IBusinessesAppService _businessesAppService;

        public IndexModel(IBusinessesAppService businessesAppService)
        {
            _businessesAppService = businessesAppService;
        }

        public async Task OnGetAsync()
        {

            await Task.CompletedTask;
        }
    }
}