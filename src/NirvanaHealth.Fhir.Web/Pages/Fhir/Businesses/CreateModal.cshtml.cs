using NirvanaHealth.Fhir.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NirvanaHealth.Fhir.Businesses;

namespace NirvanaHealth.Fhir.Web.Pages.Fhir.Businesses
{
    public class CreateModalModel : FhirPageModel
    {
        [BindProperty]
        public BusinessCreateDto Business { get; set; }

        private readonly IBusinessesAppService _businessesAppService;

        public CreateModalModel(IBusinessesAppService businessesAppService)
        {
            _businessesAppService = businessesAppService;
        }

        public async Task OnGetAsync()
        {
            Business = new BusinessCreateDto();

            await Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {

            await _businessesAppService.CreateAsync(Business);
            return NoContent();
        }
    }
}