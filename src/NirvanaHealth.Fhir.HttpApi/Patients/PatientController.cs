using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Hl7.Fhir.Model;
using Microsoft.Extensions.Configuration;
using Hl7.Fhir.Serialization;
using NirvanaHealth.Fhir.EsmMembers;

namespace NirvanaHealth.Fhir.Patients
{
    [RemoteService(Name = "Fhir")]
    [Area("fhir")]
    [ControllerName(nameof(ResourceType.Patient))]
    [Route("api/fhir/patients")]
    public class PatientsController : FhirController
    {
        private readonly ClientConfiguration _externalClients;
        private readonly IConfiguration _configuration;
        private readonly IEsmMembersAppService  _esmMembersAppService;
        public PatientsController(ClientConfiguration externalClients, IConfiguration configuration, IEsmMembersAppService esmMembersAppService)
        {
            _externalClients = externalClients;
            _configuration = configuration;
            _esmMembersAppService = esmMembersAppService;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetAsync(string id = "1a4277d565f7284c98019825981e61b1c47293d197f0216f114f8e272d763d22")
        {

            var fhir = _externalClients.Fhir[0];
            var proxyServ = _configuration["AuthServer:Proxy"];
            fhir = await FhirAuthExtension.RequestTokenAsync(fhir, proxyServ);

            if (!fhir.HasError)
            {
                var fhirClient = new Microsoft.Health.Fhir.Client.FhirClient(fhir, proxyServ, Hl7.Fhir.Rest.ResourceFormat.Json);
                Patient patient = await fhirClient.SearchPatientAsync(ResourceType.Patient, "/R4/Patient/" + id);
                return Ok(await FhirResponse(patient));
            }
            else
            {
                return BadRequest(fhir.ErrorMsg);
            }
        }

        [HttpPost]
        [Route("medical/patient-sync")]
        public async Task<ActionResult> PatientSyncAsync(string token, string id)
        {
            var result = await _esmMembersAppService.GetAsync(id);

            if (result == null)
                return BadRequest("No Payer/Bussiness are found gievn id");
            return await Postpatient(result, token);
        }

        //[HttpPost]
        //[Route("medical/patient-sync/mcp-memberid")]
        //public async Task<ActionResult> PatientSyncAsync(string token, int mcpMemberID)
        //{
        //    var result = await _esmMembersAppService.GetAsync(mcpMemberID);

        //    if (result == null)
        //        return BadRequest("No Payer/Bussiness are found gievn id");
        //    return await Postpatient(result, token);
        //}


        [HttpPost]
        [Route("medical/patient")]
        public async Task<ActionResult> PostpatientAsync(string token, Hl7.Fhir.Model.Patient patient)
        {
            if (patient == null)
                return BadRequest("Please provide the valid patient or date should not be empty.");
            return await Postpatient(patient, token);
        }


        private async Task<ActionResult> Postpatient(Hl7.Fhir.Model.Patient result, string token)
        {
            var fhirSerializer = new FhirJsonSerializer();
            var fhir = _externalClients.Fhir[1];
            var proxyServ = _configuration["AuthServer:Proxy"];
            if (!string.IsNullOrEmpty(token))
                fhir.AccessToken = token;
            else
                fhir = await FhirAuthExtension.RequestTokenAsync(fhir, proxyServ);
            var fhirClient = new Microsoft.Health.Fhir.Client.FhirClient(fhir, proxyServ, Hl7.Fhir.Rest.ResourceFormat.Json);
            return Ok(await fhirClient.PostAsync("Patient", fhirSerializer.SerializeToString(result)));
        }
    }
}