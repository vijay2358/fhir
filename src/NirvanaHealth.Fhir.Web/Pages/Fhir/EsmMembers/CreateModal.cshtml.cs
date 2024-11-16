using NirvanaHealth.Fhir.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NirvanaHealth.Fhir.EsmMembers;

namespace NirvanaHealth.Fhir.Web.Pages.Fhir.EsmMembers
{
    public class CreateModalModel : FhirPageModel
    {
        [BindProperty]
        public EsmMemberCreateDto EsmMember { get; set; }

        private readonly IEsmMembersAppService _esmMembersAppService;

        public CreateModalModel(IEsmMembersAppService esmMembersAppService)
        {
            _esmMembersAppService = esmMembersAppService;
        }

        public async Task OnGetAsync()
        {
            EsmMember = new EsmMemberCreateDto();

            await Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {

            await _esmMembersAppService.CreateAsync(EsmMember);
            return NoContent();
        }
    }
}