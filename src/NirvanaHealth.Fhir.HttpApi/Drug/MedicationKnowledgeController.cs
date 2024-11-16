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

namespace NirvanaHealth.Fhir.MedicationKnowledge
{
    [RemoteService(Name = "Fhir")]
    [Area("fhir")]
    [ControllerName(nameof(ResourceType.MedicationKnowledge))]
    [Route("api/fhir/MedicationKnowledge")]
    public class MedicationKnowledgeController : FhirController
    {
        private readonly ClientConfiguration _externalClients;
        IConfiguration _configuration;

        public MedicationKnowledgeController(ClientConfiguration externalClients, IConfiguration configuration)
        {
            _externalClients = externalClients;
            _configuration = configuration;
        }


        [HttpGet]
        public async Task<ActionResult> GetMedicationKnowledgeAsync(string QueryString = "?_profile=http://hl7.org/fhir/us/davinci-drug-formulary/StructureDefinition/usdf-FormularyDrug&DrugPlan=H1280001000")
        {

            var fhir = _externalClients.Fhir[0];
            var proxyServ = _configuration["AuthServer:Proxy"];
            fhir = await FhirAuthExtension.RequestTokenAsync(fhir, proxyServ);
            if (!fhir.HasError)
            {
                var fhirClient = new Microsoft.Health.Fhir.Client.FhirClient(fhir, proxyServ, Hl7.Fhir.Rest.ResourceFormat.Json);
                Bundle bundle = await fhirClient.SearchAsync(ResourceType.MedicationKnowledge, QueryString);
                return Ok(await FhirResponse(bundle));
            }
            else
            {
                return BadRequest(fhir.ErrorMsg);
            }
        }    
    }
}