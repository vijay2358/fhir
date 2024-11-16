using NirvanaHealth.Fhir;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Hl7.Fhir.Model;
using NirvanaHealth.Fhir.Businesses;
using Microsoft.Extensions.Configuration;

namespace NirvanaHealth.Fhir.EsmMembers
{
    public class EsmMemberManager : DomainService
    {
        private readonly IEsmMemberRepository _esmMemberRepository;
        private readonly CommonDomainManager _commonDomainManager;
        private readonly IConfiguration _configuration;

        public EsmMemberManager(IEsmMemberRepository esmMemberRepository, CommonDomainManager commonDomainManager, IConfiguration configuration)
        {
            _esmMemberRepository = esmMemberRepository;
            _commonDomainManager = commonDomainManager;
            _configuration = configuration;

        }


        public Task<Patient> MapPatient(EsmMember result)
        {
            var patient = new Patient()
            {
                Id = result.Id
            };
            var patientIdentifier = new List<Hl7.Fhir.Model.Identifier>();
            //System ID 
            patientIdentifier.Add(_commonDomainManager.GetIdentifier(result.Id));
            patientIdentifier.Add(MCPIdentityfier(new Coding("http://terminology.hl7.org/CodeSystem/v2-0203", "MB"), _configuration["FhirServer:System"] + "/fhir/memberidentifier", result.MCPMember_ID.ToString()));
            patient.Identifier = patientIdentifier;
            patient.Name = new List<HumanName>
            {
                new HumanName
                {
                    Family = result.LastName,
                    Given = new List<string>
                    {
                       result.FirstName,
                    },
                    Suffix = new List<string>
                    {
                       result.Suffix,
                    },
                    Prefix = new List<string>
                    {
                       result.PreFix,
                    },
                }
            };
            patient.Meta = new Meta()
            {
                LastUpdated = DateTime.Now,
                Profile = new List<string>
                {
                    "http://hl7.org/fhir/us/carin-bb/StructureDefinition/C4BB-Patient",
                    "http://hl7.org/fhir/us/carin-bb/StructureDefinition/C4BB-Patient|1.0.0"
                }
            };
            patient.Gender = GetMemberGendar(result.Gender);
            patient.BirthDate = result.DOB.HasValue ? result.DOB.Value.ToString("dd/MM/yyyy") : string.Empty;
            patient.MaritalStatus = new CodeableConcept //TODO MISSING THIS FILED IN MCP MEMBER 
            {
                Coding = new List<Coding>()
                {
                     new Coding()
                     {
                         System = "http://terminology.hl7.org/CodeSystem/v3-NullFlavor",
                         Code="UNK",
                     }
                }
            };
            if (result.Addresses != null && result.Addresses.Count() > 0)
                foreach (var address in result.Addresses)
                {
                    var add1 = !String.IsNullOrEmpty(address.Address1) ? address.Address1.ToUpper() : String.Empty;
                    var add2 = !String.IsNullOrEmpty(address.Address2) ? add1 + "  " + address.Address2.ToUpper() : add1;
                    patient.Address.Add(new Address()
                    {
                        Line = new List<string> { add2 },
                        City = address.City,
                        State = address.State,
                        PostalCode = address.Zip,
                        Country = address.Country
                    });
                };
            patient.Active = true; // TODO: CURRENLTY ACTIVE FOR ALL
            if (result.OtherIdentities != null && result.OtherIdentities.Count() > 0)
                foreach (var id in result.OtherIdentities)
                {
                    patient.Identifier.Add(MCPOtherIdentityfier(_configuration["FhirServer:System"] + "/fhir/", id));
                };

            return System.Threading.Tasks.Task.FromResult(patient);
        }

        private Identifier MCPIdentityfier(Coding coding, string system, string value)
        {
            return new Identifier
            {
                Type = new CodeableConcept
                {
                    Coding = new List<Coding>() {
                        coding
                    },
                },
                System = system,// _configuration["FhirServer:System"] + "/fhir/memberidentifier",
                Value = value,//result.Id
            };
        }

        private Identifier MCPOtherIdentityfier(string system, OtherIdentifier otherIdentities)
        {
            if (otherIdentities == null)
                return null;
            return new Identifier
            {
                System = system + otherIdentities.IdentityType.Name,// _configuration["FhirServer:System"] + "/fhir/memberidentifier",
                Value = otherIdentities.Value,//result.Id
            };
        }

        private AdministrativeGender GetMemberGendar(Gender gender)
        {
            switch (gender)
            {
                case Gender.Male:
                    return AdministrativeGender.Male;
                case Gender.Female:
                    return AdministrativeGender.Female;
                case Gender.Unknown:
                    return AdministrativeGender.Unknown;
                default:
                    return AdministrativeGender.Unknown;
            }
        }

        public async Task<EsmMember> CreateAsync(
        int mCPMember_ID, string lastName, string firstName, string mIddleName, string suffix, string preFix, Gender gender, DateTime? dOB = null, DateTime? dateofDeath = null, Race? race = null)
        {
            var esmMember = new EsmMember(
            
             mCPMember_ID, lastName, firstName, mIddleName, suffix, preFix, gender, dOB, dateofDeath, race
             );

            return await _esmMemberRepository.InsertAsync(esmMember);
        }

        public async Task<EsmMember> UpdateAsync(
            string id,
            int mCPMember_ID, string lastName, string firstName, string mIddleName, string suffix, string preFix, Gender gender, DateTime? dOB = null, DateTime? dateofDeath = null, Race? race = null
        )
        {
            var queryable = await _esmMemberRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var esmMember = await AsyncExecuter.FirstOrDefaultAsync(query);

            esmMember.MCPMember_ID = mCPMember_ID;
            esmMember.LastName = lastName;
            esmMember.FirstName = firstName;
            esmMember.MIddleName = mIddleName;
            esmMember.Suffix = suffix;
            esmMember.PreFix = preFix;
            esmMember.Gender = gender;
            esmMember.DOB = dOB;
            esmMember.DateofDeath = dateofDeath;
            esmMember.Race = race;

            return await _esmMemberRepository.UpdateAsync(esmMember);
        }

    }
}