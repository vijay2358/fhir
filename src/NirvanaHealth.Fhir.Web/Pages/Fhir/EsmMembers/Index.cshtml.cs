using NirvanaHealth.Fhir;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using NirvanaHealth.Fhir.EsmMembers;
using NirvanaHealth.Fhir.Shared;

namespace NirvanaHealth.Fhir.Web.Pages.Fhir.EsmMembers
{
    public class IndexModel : AbpPageModel
    {
        public int? MCPMember_IDFilterMin { get; set; }

        public int? MCPMember_IDFilterMax { get; set; }
        public string LastNameFilter { get; set; }
        public string FirstNameFilter { get; set; }
        public string MIddleNameFilter { get; set; }
        public string SuffixFilter { get; set; }
        public string PreFixFilter { get; set; }
        public DateTime? DOBFilterMin { get; set; }

        public DateTime? DOBFilterMax { get; set; }
        public Gender? GenderFilter { get; set; }
        public DateTime? DateofDeathFilterMin { get; set; }

        public DateTime? DateofDeathFilterMax { get; set; }
        public Race? RaceFilter { get; set; }

        private readonly IEsmMembersAppService _esmMembersAppService;

        public IndexModel(IEsmMembersAppService esmMembersAppService)
        {
            _esmMembersAppService = esmMembersAppService;
        }

        public async Task OnGetAsync()
        {

            await Task.CompletedTask;
        }
    }
}