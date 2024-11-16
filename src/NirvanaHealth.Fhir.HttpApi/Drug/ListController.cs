using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using NirvanaHealth.Fhir.Patients;
using Hl7.Fhir.Model;
using System.Net.Http;
using IdentityModel.Client;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace NirvanaHealth.Fhir.List
{
    [RemoteService(Name = "Fhir")]
    [Area("fhir")]
    [ControllerName(nameof(ResourceType.List))]
    [Route("api/fhir/List")]
    public class ListController : FhirController
    {
        private readonly ClientConfiguration _externalClients;
        IConfiguration _configuration;

        public ListController(ClientConfiguration externalClients, IConfiguration configuration)
        {
            _externalClients = externalClients;
            _configuration = configuration;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetAsync(string id = "?_profile=http://hl7.org/fhir/us/davinci-drug-formulary/StructureDefinition/usdf-CoveragePlan&_id=FFCBEEBC-DAEA-40D5-B2BF-C24409EB96A4")
        {
            var fhir = _externalClients.Fhir[0];
            var proxyServ = _configuration["AuthServer:Proxy"];
            fhir = await FhirAuthExtension.RequestTokenAsync(fhir, proxyServ);
            if (!fhir.HasError)
            {
                var fhirClient = new Microsoft.Health.Fhir.Client.FhirClient(fhir, proxyServ, Hl7.Fhir.Rest.ResourceFormat.Json);
                Hl7.Fhir.Model.List list = await fhirClient.ReadAsync<Hl7.Fhir.Model.List>(ResourceType.List, id);
                return Ok( await FhirResponse(list));
            }
            else
            {
                return BadRequest(fhir.ErrorMsg);
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetidentifierAsync(string identifier = "?_profile=http://hl7.org/fhir/us/davinci-drug-formulary/StructureDefinition/usdf-CoveragePlan")
        {
            var fhir = _externalClients.Fhir[0];
            var proxyServ = _configuration["AuthServer:Proxy"];
            fhir = await FhirAuthExtension.RequestTokenAsync(fhir, proxyServ);
            if (!fhir.HasError)
            {
                var fhirClient = new Microsoft.Health.Fhir.Client.FhirClient(fhir, proxyServ, Hl7.Fhir.Rest.ResourceFormat.Json);
                Bundle bundle = await fhirClient.SearchAsync(ResourceType.List, identifier);
                return Ok(await FhirResponse(bundle));
            }
            else
            {
                return BadRequest(fhir.ErrorMsg);
            }
        }
    }
}