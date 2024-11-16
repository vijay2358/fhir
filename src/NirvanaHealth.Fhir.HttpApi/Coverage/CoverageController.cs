using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Hl7.Fhir.Model;
using Microsoft.Extensions.Configuration;
using System.Linq;
using NirvanaHealth.Fhir.Businesses;
using Hl7.Fhir.Serialization;
using NirvanaHealth.Fhir.MbrEnrollDetails;

namespace NirvanaHealth.Fhir.Organization
{
    [RemoteService(Name = "Fhir")]
    [Area("fhir")]
    [ControllerName(nameof(ResourceType.Organization))]
    [Route("api/fhir/Coverage")]
    public class CoverageController : FhirController
    {
        private readonly ClientConfiguration _externalClients;
        IConfiguration _configuration;
        private readonly IMbrEnrollDetailsAppService  _mbrEnrollDetailsAppService;
        public CoverageController(ClientConfiguration externalClients, IConfiguration configuration, IMbrEnrollDetailsAppService mbrEnrollDetailsAppService)
        {
            _externalClients = externalClients;
            _configuration = configuration;
            _mbrEnrollDetailsAppService = mbrEnrollDetailsAppService;
        }

        [HttpPost]
        [Route("medical/coverage-sync")]
        public async Task<ActionResult> CoverageSyncAsync(string token, string id)
        {
            var result = await _mbrEnrollDetailsAppService.GetAsync(id);

            if (result == null)
                return BadRequest("No Payer/Bussiness are found gievn id");
            return await PostCoverage(result, token);
        }


        [HttpPost]
        [Route("medical/coverage")]
        public async Task<ActionResult> PostCoverageAsync(string token, Hl7.Fhir.Model.Coverage coverage)
        {
            if (coverage == null)
                return BadRequest("Please provide the valid coverage or date should not be empty.");
            return await PostCoverage(coverage, token);
        }

        
        private async Task<ActionResult> PostCoverage(Hl7.Fhir.Model.Coverage result, string token)
        {
            var fhirSerializer = new FhirJsonSerializer();
            var fhir = _externalClients.Fhir[1];
            var proxyServ = _configuration["AuthServer:Proxy"];
            if (!string.IsNullOrEmpty(token))
                fhir.AccessToken = token;
            else
                fhir = await FhirAuthExtension.RequestTokenAsync(fhir, proxyServ);            
            var fhirClient = new Microsoft.Health.Fhir.Client.FhirClient(fhir, proxyServ, Hl7.Fhir.Rest.ResourceFormat.Json);
            var organization = await fhirClient.PostAsync("Coverage", fhirSerializer.SerializeToString(result));
            return Ok(organization);
        }


    }
}