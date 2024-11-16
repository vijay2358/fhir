using NirvanaHealth.Fhir.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using NirvanaHealth.Fhir.EsmMembers;

namespace NirvanaHealth.Fhir.Web.Pages.Fhir.EsmMembers
{
    public class EditModalModel : FhirPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public EsmMemberUpdateDto EsmMember { get; set; }

        private readonly IEsmMembersAppService _esmMembersAppService;

        public EditModalModel(IEsmMembersAppService esmMembersAppService)
        {
            _esmMembersAppService = esmMembersAppService;
        }

        public async Task OnGetAsync()
        {
            var esmMember = await _esmMembersAppService.GetAsync(Id);
            EsmMember = ObjectMapper.Map<EsmMemberDto, EsmMemberUpdateDto>(esmMember);

        }

        public async Task<IActionResult> OnPostAsync()
        {

            await _esmMembersAppService.UpdateAsync(Id, EsmMember);
            return NoContent();
        }
    }
}