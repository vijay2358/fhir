using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Hl7.Fhir.Model;
using Microsoft.Extensions.Configuration;

namespace NirvanaHealth.Fhir.HealthcareService
{
    [RemoteService(Name = "Fhir")]
    [Area("fhir")]
    [ControllerName(nameof(ResourceType.HealthcareService))]
    [Route("api/fhir/HealthcareService")]
    public class HealthcareServiceController : FhirController
    {
        private readonly ClientConfiguration _externalClients;
        IConfiguration _configuration;

        public HealthcareServiceController(ClientConfiguration externalClients, IConfiguration configuration)
        {
            _externalClients = externalClients;
            _configuration = configuration;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetAsync(string id = "62c680b9a29b2183cd8873736135ded168efcb4e0086f036373940994b86416f")
        {            
            var fhir = _externalClients.Fhir[0];
            var proxyServ = _configuration["AuthServer:Proxy"];
            fhir = await FhirAuthExtension.RequestTokenAsync(fhir, proxyServ);
            if (!fhir.HasError)
            {
                var fhirClient = new Microsoft.Health.Fhir.Client.FhirClient(fhir, proxyServ, Hl7.Fhir.Rest.ResourceFormat.Json);
                Hl7.Fhir.Model.HealthcareService healthCareServ = await fhirClient.ReadAsync<Hl7.Fhir.Model.HealthcareService>(ResourceType.HealthcareService, id);
                return Ok(await FhirResponse(healthCareServ));
            }
            else
            {
                return BadRequest(fhir.ErrorMsg);
            }
        }

        [HttpGet]      
        public async Task<ActionResult> GetHealthcareServiceAsync(string filter = "?service-category=pharm&_id=62c680b9a29b2183cd8873736135ded168efcb4e0086f036373940994b86416f")
        {
            var fhir = _externalClients.Fhir[0];
            var proxyServ = _configuration["AuthServer:Proxy"];
            fhir = await FhirAuthExtension.RequestTokenAsync(fhir, proxyServ);
            if (!fhir.HasError)
            {
                var fhirClient = new Microsoft.Health.Fhir.Client.FhirClient(fhir, proxyServ, Hl7.Fhir.Rest.ResourceFormat.Json);
                Bundle bundle = await fhirClient.SearchAsync(ResourceType.HealthcareService, filter);
                return Ok(await FhirResponse(bundle));
            }
            else
            {
                return BadRequest(fhir.ErrorMsg);
            }
        }   
    }
}