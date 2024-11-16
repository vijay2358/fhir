using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Hl7.Fhir.Model;
using Microsoft.Extensions.Configuration;

namespace NirvanaHealth.Fhir.ExplanationOfBenefit
{
    [RemoteService(Name = "Fhir")]
    [Area("fhir")]
    [ControllerName(nameof(ResourceType.ExplanationOfBenefit))]
    [Route("api/fhir/EOB")]
    public class ExplanationOfBenefitController : FhirController
    {
        private readonly ClientConfiguration _externalClients;
        IConfiguration _configuration;

        public ExplanationOfBenefitController(ClientConfiguration externalClients, IConfiguration configuration)
        {
            _externalClients = externalClients;
            _configuration = configuration;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetAsync(string id = "146633a327847923a3f09eb716a0363c238cd26fe3210a3fc8bc34c574d496a1")
        {
            var fhir = _externalClients.Fhir[0];
            var proxyServ = _configuration["AuthServer:Proxy"];
            fhir = await FhirAuthExtension.RequestTokenAsync(fhir, proxyServ);
            if (!fhir.HasError)
            {
                var fhirClient = new Microsoft.Health.Fhir.Client.FhirClient(fhir, proxyServ, Hl7.Fhir.Rest.ResourceFormat.Json);
                Hl7.Fhir.Model.ExplanationOfBenefit eob = await fhirClient.ReadAsync<Hl7.Fhir.Model.ExplanationOfBenefit>(ResourceType.ExplanationOfBenefit, id);
                return Ok(await FhirResponse(eob));

            }
            else
            {
                return BadRequest(fhir.ErrorMsg);
            }

        }

        [HttpGet]

        public async Task<ActionResult> GetEobAsync(string filter = "patient=f499f1dfe232e56c81a81727e1bffc8463262ab96d710bf641e64937566a2c4d")
        {
            var fhir = _externalClients.Fhir[0];
            var proxyServ = _configuration["AuthServer:Proxy"];
            fhir = await FhirAuthExtension.RequestTokenAsync(fhir, proxyServ);
            if (!fhir.HasError)
            {
                var fhirClient = new Microsoft.Health.Fhir.Client.FhirClient(fhir, proxyServ, Hl7.Fhir.Rest.ResourceFormat.Json);
                Bundle eob = await fhirClient.SearchAsync(ResourceType.ExplanationOfBenefit, filter);
                return Ok(await FhirResponse(eob));
            }
            else
            {
                return BadRequest(fhir.ErrorMsg);
            }
        }      
    }
}