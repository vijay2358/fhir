using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Hl7.Fhir.Model;
using NirvanaHealth.Fhir;
using Microsoft.Extensions.Configuration;

namespace NirvanaHealth.FhirExternal.Organization
{
    [RemoteService(Name = "FhirExternal")]
    [Area("fhir")]
    [ControllerName(nameof(ResourceType.Coverage))]
    [Route("api/fhir/r4/Coverage")]
    public class CoverageController : FhirExternalController
    {

        private readonly ClientConfiguration _externalClients;
        private readonly IConfiguration _configuration;
        public CoverageController(ClientConfiguration externalClients, IConfiguration configuration)
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
                Coverage Coverage = await fhirClient.ReadAsync<Hl7.Fhir.Model.Coverage>(ResourceType.Coverage, id);
                return Ok(await FhirResponse(Coverage));
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
                Bundle Coverage = await fhirClient.SearchAsync(ResourceType.Coverage, filter);
                return Ok(await FhirResponse(Coverage));
            }
            else
            {
                return BadRequest(fhir.ErrorMsg);
            }
        }

    }
}