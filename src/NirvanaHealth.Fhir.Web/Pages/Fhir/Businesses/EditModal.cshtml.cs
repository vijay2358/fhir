using NirvanaHealth.Fhir.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using NirvanaHealth.Fhir.Businesses;

namespace NirvanaHealth.Fhir.Web.Pages.Fhir.Businesses
{
    public class EditModalModel : FhirPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }

        [BindProperty]
        public BusinessUpdateDto Business { get; set; }

        private readonly IBusinessesAppService _businessesAppService;

        public EditModalModel(IBusinessesAppService businessesAppService)
        {
            _businessesAppService = businessesAppService;
        }

        public async Task OnGetAsync()
        {
            var business = await _businessesAppService.GetAsync(Id);
            Business = ObjectMapper.Map<BusinessDto, BusinessUpdateDto>(business);

        }

        public async Task<IActionResult> OnPostAsync()
        {

            await _businessesAppService.UpdateAsync(Id, Business);
            return NoContent();
        }
    }
}