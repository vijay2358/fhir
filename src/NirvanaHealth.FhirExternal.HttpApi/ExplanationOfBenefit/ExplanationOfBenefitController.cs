using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Hl7.Fhir.Model;
using Microsoft.Extensions.Configuration;
using NirvanaHealth.Fhir;

namespace NirvanaHealth.FhirExternal.ExplanationOfBenefit
{
    [RemoteService(Name = "FhirExternal")]
    [Area("fhir")]
    [ControllerName(nameof(ResourceType.ExplanationOfBenefit))]
    [Route("api/fhir/r4/ExplanationOfBenefit")]
    public class ExplanationOfBenefitController : FhirExternalController
    {
        private readonly ClientConfiguration _externalClients;
        private readonly IConfiguration _configuration;
        public ExplanationOfBenefitController(ClientConfiguration externalClients, IConfiguration configuration)
        {
            _externalClients = externalClients;
            _configuration = configuration;
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetAsync(string id)
        {
            var fhir = _externalClients.Fhir[1];
            var proxyServ = _configuration["AuthServer:Proxy"];
            fhir = await FhirAuthExtension.RequestTokenAsync(fhir, proxyServ);
            if (!fhir.HasError)
            {
                var fhirClient = new Microsoft.Health.Fhir.Client.FhirClient(fhir, proxyServ, Hl7.Fhir.Rest.ResourceFormat.Json);
                Hl7.Fhir.Model.ExplanationOfBenefit patient = await fhirClient.ReadAsync<Hl7.Fhir.Model.ExplanationOfBenefit>(ResourceType.ExplanationOfBenefit, id);
                return Ok(await FhirResponse(patient));
            }
            else
            {
                return BadRequest(fhir.ErrorMsg);
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetPatientByEverythingAsync(string filter)
        {
            var fhir = _externalClients.Fhir[1];
            var proxyServ = _configuration["AuthServer:Proxy"];
            fhir = await FhirAuthExtension.RequestTokenAsync(fhir, proxyServ);
            if (!fhir.HasError)
            {
                var fhirClient = new Microsoft.Health.Fhir.Client.FhirClient(fhir, proxyServ, Hl7.Fhir.Rest.ResourceFormat.Json);
                Bundle patient = await fhirClient.SearchAsync(ResourceType.ExplanationOfBenefit, filter);
                return Ok(await FhirResponse(patient));
            }
            else
            {
                return BadRequest(fhir.ErrorMsg);
            }
        }

    }
}