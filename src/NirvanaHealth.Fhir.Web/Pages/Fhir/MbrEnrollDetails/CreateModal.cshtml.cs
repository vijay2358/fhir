using NirvanaHealth.Fhir.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NirvanaHealth.Fhir.MbrEnrollDetails;

namespace NirvanaHealth.Fhir.Web.Pages.Fhir.MbrEnrollDetails
{
    public class CreateModalModel : FhirPageModel
    {
        [BindProperty]
        public MbrEnrollDetailCreateDto MbrEnrollDetail { get; set; }

        private readonly IMbrEnrollDetailsAppService _mbrEnrollDetailsAppService;

        public CreateModalModel(IMbrEnrollDetailsAppService mbrEnrollDetailsAppService)
        {
            _mbrEnrollDetailsAppService = mbrEnrollDetailsAppService;
        }

        public async Task OnGetAsync()
        {
            MbrEnrollDetail = new MbrEnrollDetailCreateDto();

            await Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {

            await _mbrEnrollDetailsAppService.CreateAsync(MbrEnrollDetail);
            return NoContent();
        }
    }
}