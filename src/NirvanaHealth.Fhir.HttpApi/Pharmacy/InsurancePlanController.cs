using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Hl7.Fhir.Model;
using Microsoft.Extensions.Configuration;

namespace NirvanaHealth.Fhir.InsurancePlan
{
    [RemoteService(Name = "Fhir")]
    [Area("fhir")]
    [ControllerName(nameof(ResourceType.InsurancePlan))]
    [Route("api/fhir/InsurancePlan")]
    public class InsurancePlanController : FhirController
    {
        private readonly ClientConfiguration _externalClients;
        IConfiguration _configuration;

        public InsurancePlanController(ClientConfiguration externalClients, IConfiguration configuration)
        {
            _externalClients = externalClients;
            _configuration = configuration;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetAsync(string id = "")
        {
            var fhir = _externalClients.Fhir[0];
            var proxyServ = _configuration["AuthServer:Proxy"];
            fhir = await FhirAuthExtension.RequestTokenAsync(fhir, proxyServ);
            if (!fhir.HasError)
            {
                var fhirClient = new Microsoft.Health.Fhir.Client.FhirClient(fhir, proxyServ, Hl7.Fhir.Rest.ResourceFormat.Json);
                Hl7.Fhir.Model.InsurancePlan insurancePlan = await fhirClient.ReadAsync<Hl7.Fhir.Model.InsurancePlan>(ResourceType.InsurancePlan, id);
                return Ok(await FhirResponse(insurancePlan));
            }
            else
            {
                return BadRequest(fhir.ErrorMsg);
            }

        }

        [HttpGet]
        public async Task<ActionResult> GetInsurancePlanAsync(string filter = "?type=medid&owned-by=eternal")
        {

            var fhir = _externalClients.Fhir[0];
            var proxyServ = _configuration["AuthServer:Proxy"];
            fhir = await FhirAuthExtension.RequestTokenAsync(fhir, proxyServ);
            if (!fhir.HasError)
            {
                var fhirClient = new Microsoft.Health.Fhir.Client.FhirClient(fhir, proxyServ, Hl7.Fhir.Rest.ResourceFormat.Json);
                Bundle bundle = await fhirClient.SearchAsync(ResourceType.InsurancePlan, filter);
                return Ok(await FhirResponse(bundle));
            }
            else
            {
                return BadRequest(fhir.ErrorMsg);
            }
        }
    }
}