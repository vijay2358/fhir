using System.Collections.Generic;
using System.Threading.Tasks;
using Hl7.Fhir.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;

namespace NirvanaHealth.Fhir.Samples;

public class FhirAuthTokenAppService : FhirAppService, IFhirAuthTokenAppService
{
    ClientConfiguration _externalClients;
    IConfiguration _configuration;
    public FhirAuthTokenAppService(ClientConfiguration externalClients, IConfiguration configuration)
    {
        _externalClients = externalClients;
        _configuration = configuration;
    }

    [Authorize]
    public Task<Fhir> GetToken()
    {
        var fhir = _externalClients.Fhir[0];
        var proxyServ = _configuration["AuthServer:Proxy"];
        var authtoken = FhirAuthExtension.RequestTokenAsync(fhir, proxyServ);

        if (!authtoken.Result.HasError)
        {
            fhir.AccessToken = authtoken.Result.AccessToken;
            return System.Threading.Tasks.Task.FromResult(authtoken.Result);
        }
        else
            return System.Threading.Tasks.Task.FromResult(authtoken.Result);

    }

    [Authorize]
    public Task<List<Patient>> PostPatients()
    {
        ///var members = _repo.getESMMember()//

        //STUB PATIENTS;
        //stub1
        var stub1 = new Patient();
        var name1 = new HumanName();
        //name1.Family = new List<string> { "Odenkirk" };
        name1.Given = new List<string> { "Bob" };

        stub1.Name = new List<HumanName> { name1 };
        stub1.Id = "b3tter_c@ll_$@ul";
        stub1.Gender = AdministrativeGender.Male;

        //stub2
        var stub2 = new Patient();
        var name2 = new HumanName();
        //name2.Family = new List<string> { "Saggot" };
        name2.Given = new List<string> { "Bob" };

        stub2.Name = new List<HumanName> { name2 };
        stub2.Id = "full_house789";
        stub2.Gender = AdministrativeGender.Male;

        //stub3
        var stub3 = new Patient();
        var name3 = new HumanName();
        //name3.Family = new List<string> { "Jones" };
        name3.Given = new List<string> { "Jessica" };

        stub3.Name = new List<HumanName> { name3 };
        stub3.Id = "m@r3lou$";
        stub3.Gender = AdministrativeGender.Female;

        return System.Threading.Tasks.Task.FromResult(new List<Patient> { stub1, stub2, stub3 });

    }
}