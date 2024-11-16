using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Hl7.Fhir.Model;
using Microsoft.Extensions.Configuration;

namespace NirvanaHealth.Fhir.Location
{
    [RemoteService(Name = "Fhir")]
    [Area("fhir")]
    [ControllerName("Location")]
    [Route("api/fhir/Location")]
    public class LocationDirectoryController : FhirController
    {
        private readonly ClientConfiguration _externalClients;
        IConfiguration _configuration;

        public LocationDirectoryController(ClientConfiguration externalClients, IConfiguration configuration)
        {
            _externalClients = externalClients;
            _configuration = configuration;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetAsync(string id = "d6e7c54ba8da041f975c43a2c7543b694416f419ac691c5e30090d3d4a58a782")
        {
            var fhir = _externalClients.Fhir[0];
            var proxyServ = _configuration["AuthServer:Proxy"];
            fhir = await FhirAuthExtension.RequestTokenAsync(fhir, proxyServ);
            if (!fhir.HasError)
            {
                var fhirClient = new Microsoft.Health.Fhir.Client.FhirClient(fhir, proxyServ, Hl7.Fhir.Rest.ResourceFormat.Json);
                Hl7.Fhir.Model.Location location = await fhirClient.ReadAsync<Hl7.Fhir.Model.Location>(ResourceType.Location, id);
                return Ok(await FhirResponse(location));
            }
            else
            {
                return BadRequest(fhir.ErrorMsg);
            }
           
        }

        [HttpGet]
        public async Task<ActionResult> GetLocationAsync(string filter = "?type=PHARM&_id=d6e7c54ba8da041f975c43a2c7543b694416f419ac691c5e30090d3d4a58a782")
        {
            var fhir = _externalClients.Fhir[0];
            var proxyServ = _configuration["AuthServer:Proxy"];
            fhir = await FhirAuthExtension.RequestTokenAsync(fhir, proxyServ);
            if (!fhir.HasError)
            {
                var fhirClient = new Microsoft.Health.Fhir.Client.FhirClient(fhir, proxyServ, Hl7.Fhir.Rest.ResourceFormat.Json);
                Bundle bundle = await fhirClient.SearchAsync(ResourceType.Location, filter);
                return Ok(await FhirResponse(bundle));
            }
            else
            {
                return BadRequest(fhir.ErrorMsg);
            }
        }       
    }
}