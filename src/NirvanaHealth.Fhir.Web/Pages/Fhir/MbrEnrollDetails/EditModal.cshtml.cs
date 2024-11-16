using NirvanaHealth.Fhir.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using NirvanaHealth.Fhir.MbrEnrollDetails;

namespace NirvanaHealth.Fhir.Web.Pages.Fhir.MbrEnrollDetails
{
    public class EditModalModel : FhirPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }

        [BindProperty]
        public MbrEnrollDetailUpdateDto MbrEnrollDetail { get; set; }

        private readonly IMbrEnrollDetailsAppService _mbrEnrollDetailsAppService;

        public EditModalModel(IMbrEnrollDetailsAppService mbrEnrollDetailsAppService)
        {
            _mbrEnrollDetailsAppService = mbrEnrollDetailsAppService;
        }

        public async Task OnGetAsync()
        {
            var mbrEnrollDetail = await _mbrEnrollDetailsAppService.GetAsync(Id);
            MbrEnrollDetail = ObjectMapper.Map<MbrEnrollDetailDto, MbrEnrollDetailUpdateDto>(mbrEnrollDetail);

        }

        public async Task<IActionResult> OnPostAsync()
        {

            await _mbrEnrollDetailsAppService.UpdateAsync(Id, MbrEnrollDetail);
            return NoContent();
        }
    }
}