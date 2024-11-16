using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hl7.Fhir.Model;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace NirvanaHealth.Fhir.Businesses
{
    public class BusinessManager : DomainService
    {
        private readonly IBusinessRepository _businessRepository;
        private readonly CommonDomainManager _commonDomainManager;
        private readonly IConfiguration _configuration;
        public BusinessManager(IBusinessRepository businessRepository, CommonDomainManager commonDomainManager, IConfiguration configuration)
        {
            _businessRepository = businessRepository;
            _commonDomainManager = commonDomainManager;
            _configuration = configuration;
        }

        public async Task<Organization> MapOrganization(Business result)
        {
            var organization = new Organization()
            {
                Id = result.Id
            };
            organization.Meta = new Meta()
            {
                LastUpdated = DateTime.Now,
                Profile = new List<string>
                {
                    "http://hl7.org/fhir/us/carin-bb/StructureDefinition/C4BB-Organization",
                    "http://hl7.org/fhir/us/carin-bb/StructureDefinition/C4BB-Organization|1.0.0"
                }
            };
            var organizationtIdentifier = new List<Hl7.Fhir.Model.Identifier>();
            //System ID 
            organizationtIdentifier.Add(_commonDomainManager.GetIdentifier(result.Id));
            organizationtIdentifier.Add(new Identifier
            {
                Type = new CodeableConcept
                {
                    Coding = new List<Coding>()
                    {
                        new Coding("http://hl7.org/fhir/us/carin-bb/CodeSystem/C4BBIdentifierType","payerid"),
                    }
                },
                System = _configuration["FhirServer:System"] + "/identifier",
                Value = result.Id
            });
            organizationtIdentifier.Add(new Identifier
            {
                System = _configuration["FhirServer:System"]+ "/payercode",
                Value = result.BusinessCode
            });
            organizationtIdentifier.Add(new Identifier
            {
                System = _configuration["FhirServer:System"] + "/payerid",
                Value = result.Business_ID.ToString()
            }); ;
            organization.Identifier = organizationtIdentifier;
            organization.Active = true; // TODO: CURRENLTY ACTIVE FOR ALL
            organization.Type = _commonDomainManager.GetTypeInlcudingCoding(OrganizationType.pay);            
            organization.Name = result.BusinessName.ToUpper();
            organization.Address = new List<Address>();

            if (result.Addresses != null && result.Addresses.Count() > 0)
                foreach (var address in result.Addresses)
                {
                    var add1 = !String.IsNullOrEmpty(address.Address1) ? address.Address1.ToUpper() : String.Empty;
                    var add2 = !String.IsNullOrEmpty(address.Address2) ? add1 + "  " + address.Address2.ToUpper() : add1;
                    organization.Address.Add(new Address()
                    {
                        Line = new List<string> { add2 },                        
                        City = address.City,
                        State = address.State,
                        PostalCode = address.Zip,
                        Country = address.Country
                    });
                };
            return organization;
        }

        public async Task<Business> CreateAsync(
        int business_ID, string businessName, string businessCode, string trackerCode, string dBA, string fEIN, int? businessCat = null)
        {
            var business = new Business(

             business_ID, businessName, businessCode, trackerCode, dBA, fEIN, businessCat
             );

            return await _businessRepository.InsertAsync(business);
        }

        public async Task<Business> UpdateAsync(
            string id,
            int business_ID, string businessName, string businessCode, string trackerCode, string dBA, string fEIN, int? businessCat = null
        )
        {
            var queryable = await _businessRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var business = await AsyncExecuter.FirstOrDefaultAsync(query);

            business.Business_ID = business_ID;
            business.BusinessName = businessName;
            business.BusinessCode = businessCode;
            business.TrackerCode = trackerCode;
            business.DBA = dBA;
            business.FEIN = fEIN;
            business.BusinessCat = businessCat;

            return await _businessRepository.UpdateAsync(business);
        }

    }
}