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
    public class CommonDomainManager : DomainService
    {
        private readonly IConfiguration _configuration;
        public CommonDomainManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Identifier GetIdentifier(string Id)
        {
            return new Identifier(_configuration["FhirServer:System"], Id);
        }

        public List<OrganizationDefinfion> OrganizationDefinfions()
        {
            return new List<OrganizationDefinfion>()
            {
                new OrganizationDefinfion(nameof(OrganizationType.prov), "HEALTHCARE PROVIDER", "An organization that provides healthcare services."),
                new OrganizationDefinfion(nameof(OrganizationType.dept), "HOSPITAL DEPARTMENT", "A department or ward within a hospital (Generally is not applicable to top level organizations)"),
                new OrganizationDefinfion(nameof(OrganizationType.team), "ORGANIZATIONAL TEAM", "A department or ward within a hospital (Generally is not applicable to top level organizations)"),
                new OrganizationDefinfion(nameof(OrganizationType.govt), "GOVERNMENT", "A department or ward within a hospital (Generally is not applicable to top level organizations)"),
                new OrganizationDefinfion(nameof(OrganizationType.ins), "INSURANCE COMPANY", "A department or ward within a hospital (Generally is not applicable to top level organizations)"),
                new OrganizationDefinfion(nameof(OrganizationType.pay), "PAYER", "A department or ward within a hospital (Generally is not applicable to top level organizations)"),
                new OrganizationDefinfion(nameof(OrganizationType.edu), "EDUCATIONAL INSTITUTE", "A department or ward within a hospital (Generally is not applicable to top level organizations)"),
                new OrganizationDefinfion(nameof(OrganizationType.reli), "RELIGIOUS INSTITUTION", "A department or ward within a hospital (Generally is not applicable to top level organizations)"),
                new OrganizationDefinfion(nameof(OrganizationType.crs), "CLINICAL RESEARCH SPONSOR", "A department or ward within a hospital (Generally is not applicable to top level organizations)"),
                new OrganizationDefinfion(nameof(OrganizationType.cg), "COMMUNITY GROUP", "A department or ward within a hospital (Generally is not applicable to top level organizations)"),
                new OrganizationDefinfion(nameof(OrganizationType.bus), "NON-HEALTHCARE BUSINESS OR CORPORATION", "A department or ward within a hospital (Generally is not applicable to top level organizations)"),
                new OrganizationDefinfion(nameof(OrganizationType.other), "OTHER", "A department or ward within a hospital (Generally is not applicable to top level organizations)"),
            };
        }

        public List<CodeableConcept> GetTypeInlcudingCoding(OrganizationType organizationType)
        {
            var coding = new Coding();
            coding.System = "http://terminology.hl7.org/CodeSystem/organization-type";
            coding.Code = organizationType.ToString();

            var text = OrganizationDefinfions().Find(x => x.Code == coding.Code).Name.ToUpper();

            return new List<CodeableConcept>
            {
                new CodeableConcept()
                {
                    Coding = new List<Coding>() { coding },
                    Text =text
                },
            };
        }

        //public Coding Coding()
        //{
        //    var coding = new Coding();
        //    coding.System = "http://terminology.hl7.org/CodeSystem/v3-ActCode";
        //    coding.Code = "ETH";
        //    Coding

        //    return coding;
        //}
    }
}