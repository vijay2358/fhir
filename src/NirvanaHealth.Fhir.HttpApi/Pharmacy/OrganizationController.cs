using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Hl7.Fhir.Model;
using Microsoft.Extensions.Configuration;
using System.Linq;
using NirvanaHealth.Fhir.Businesses;
using Hl7.Fhir.Serialization;

namespace NirvanaHealth.Fhir.Organization
{
    [RemoteService(Name = "Fhir")]
    [Area("fhir")]
    [ControllerName(nameof(ResourceType.Organization))]
    [Route("api/fhir/Organization")]
    public class OrganizationDirectoryController : FhirController
    {
        private readonly ClientConfiguration _externalClients;
        IConfiguration _configuration;
        private readonly IBusinessesAppService _businessesAppService;
        public OrganizationDirectoryController(ClientConfiguration externalClients, IConfiguration configuration, IBusinessesAppService businessesAppService)
        {
            _externalClients = externalClients;
            _configuration = configuration;
            _businessesAppService = businessesAppService;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetIdAsync(string id = "28c0d28331306d5c0eec117bd85b89eae90cfb0527c9aa65343f7832c0fe41d3")
        {
            var fhir = _externalClients.Fhir[0];
            var proxyServ = _configuration["AuthServer:Proxy"];
            fhir = await FhirAuthExtension.RequestTokenAsync(fhir, proxyServ);
            if (!fhir.HasError)
            {
                var fhirClient = new Microsoft.Health.Fhir.Client.FhirClient(fhir, proxyServ, Hl7.Fhir.Rest.ResourceFormat.Json);
                Hl7.Fhir.Model.Organization organization = await fhirClient.ReadAsync<Hl7.Fhir.Model.Organization>(ResourceType.Organization, id);
                return Ok(await FhirResponse(organization));
            }
            else
            {
                return BadRequest(fhir.ErrorMsg);
            }

        }

        [HttpGet]
        public async Task<ActionResult> GetOrganizationAsync(string filter = "?type=fac&_id=28c0d28331306d5c0eec117bd85b89eae90cfb0527c9aa65343f7832c0fe41d3")
        {
            var fhir = _externalClients.Fhir[0];
            var proxyServ = _configuration["AuthServer:Proxy"];
            fhir = await FhirAuthExtension.RequestTokenAsync(fhir, proxyServ);
            if (!fhir.HasError)
            {
                var fhirClient = new Microsoft.Health.Fhir.Client.FhirClient(fhir, proxyServ, Hl7.Fhir.Rest.ResourceFormat.Json);
                Bundle bundle = await fhirClient.SearchAsync(ResourceType.Organization, filter);
                return Ok(await FhirResponse(bundle));
            }
            else
            {
                return BadRequest(fhir.ErrorMsg);
            }
        }

        [HttpPost]
        [Route("medical/organization-sync")]
        public async Task<ActionResult> OrganizationSyncAsync(string token, string id)
        {
            var result = await _businessesAppService.GetAsync(id);

            if (result == null)
                return BadRequest("No Payer/Bussiness are found gievn id");
            return await PostOrganaztion(result, token);
        }


        [HttpPost]
        [Route("medical/organization")]
        public async Task<ActionResult> PostOrganizationAsync(string token, Hl7.Fhir.Model.Organization organization)
        {
            if (organization == null)
                return BadRequest("Please provide the valid Organization/Payer or should not be empty data");
            return await PostOrganaztion(organization, token);
        }

        
        private async Task<ActionResult> PostOrganaztion(Hl7.Fhir.Model.Organization result, string token)
        {
            var fhirSerializer = new FhirJsonSerializer();
            var fhir = _externalClients.Fhir[1];
            var proxyServ = _configuration["AuthServer:Proxy"];
            if (!string.IsNullOrEmpty(token))
                fhir.AccessToken = token;
            else
                fhir = await FhirAuthExtension.RequestTokenAsync(fhir, proxyServ);            
            var fhirClient = new Microsoft.Health.Fhir.Client.FhirClient(fhir, proxyServ, Hl7.Fhir.Rest.ResourceFormat.Json);
            var organization = await fhirClient.PostAsync("Organization", fhirSerializer.SerializeToString(result));
            return Ok(organization);
        }


    }
}