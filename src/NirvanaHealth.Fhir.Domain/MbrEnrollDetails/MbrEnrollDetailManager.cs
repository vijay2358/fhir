using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hl7.Fhir.Model;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using NirvanaHealth.Fhir.BenefitPlans;
using NirvanaHealth.Fhir.Businesses;
using NirvanaHealth.Fhir.EsmMembers;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace NirvanaHealth.Fhir.MbrEnrollDetails
{
    public class MbrEnrollDetailManager : DomainService
    {
        private readonly IMbrEnrollDetailRepository _mbrEnrollDetailRepository;
        private readonly IBusinessRepository  _businessRepository;
        private readonly IBenefitPlanRepository  _benefitPlanRepository;
        private readonly IEsmMemberRepository _esmMemberRepository;
        private readonly CommonDomainManager _commonDomainManager;
        private readonly IConfiguration _configuration;

        public MbrEnrollDetailManager(
            IMbrEnrollDetailRepository mbrEnrollDetailRepository,
            CommonDomainManager commonDomainManager, 
            IConfiguration configuration, 
            IBusinessRepository businessRepository, 
            IBenefitPlanRepository benefitPlanRepository,
            IEsmMemberRepository esmMemberRepository
            )
        {
            _mbrEnrollDetailRepository = mbrEnrollDetailRepository;
            _commonDomainManager = commonDomainManager;
            _configuration = configuration;
            _businessRepository = businessRepository;
            _benefitPlanRepository = benefitPlanRepository;
            _esmMemberRepository = esmMemberRepository;
        }

        public async Task<MbrEnrollDetail> CreateAsync(
        int mbrEnrollDetail_ID, int mCPMember_ID, string member_ID, string subscriber_ID, string personCode, int? benefitPlan_ID = null)
        {
            var mbrEnrollDetail = new MbrEnrollDetail(

             mbrEnrollDetail_ID, mCPMember_ID, member_ID, subscriber_ID, personCode, benefitPlan_ID
             );

            return await _mbrEnrollDetailRepository.InsertAsync(mbrEnrollDetail);
        }

        public async Task<MbrEnrollDetail> UpdateAsync(
            string id,
            int mbrEnrollDetail_ID, int mCPMember_ID, string member_ID, string subscriber_ID, string personCode, int? benefitPlan_ID = null
        )
        {
            var queryable = await _mbrEnrollDetailRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var mbrEnrollDetail = await AsyncExecuter.FirstOrDefaultAsync(query);

            mbrEnrollDetail.MbrEnrollDetail_ID = mbrEnrollDetail_ID;
            mbrEnrollDetail.MCPMember_ID = mCPMember_ID;
            mbrEnrollDetail.Member_ID = member_ID;
            mbrEnrollDetail.Subscriber_ID = subscriber_ID;
            mbrEnrollDetail.PersonCode = personCode;
            mbrEnrollDetail.BenefitPlan_ID = benefitPlan_ID;

            return await _mbrEnrollDetailRepository.UpdateAsync(mbrEnrollDetail);
        }

        public async Task<Coverage> MapPatientCoverage(MbrEnrollDetail result)
        {


            var benefit = new BenefitPlan();
            var business = new Business();
            var mcpMember = new EsmMember();

            if (result.BenefitPlan_ID.HasValue)
                benefit = await _benefitPlanRepository.GetBenefitPlanByIdAsync(result.BenefitPlan_ID.Value);

            if (benefit != null && benefit.Business.Business_ID > 0)
            {
                business = await _businessRepository.GetBusinssByIdAsync(benefit.Business.Business_ID);
            }

            if (result.MCPMember_ID > 0)
            {
                mcpMember = await _esmMemberRepository.GetMemberByIdAsync(result.MCPMember_ID);
            }

            var coverage = new Coverage()
            {
                Id = result.Id
            };
            var coverageIdentifier = new List<Hl7.Fhir.Model.Identifier>();
            //System ID 
            coverageIdentifier.Add(_commonDomainManager.GetIdentifier(result.Id));
            coverageIdentifier.Add(CoverageIdentityfier(new Coding("http://terminology.hl7.org/CodeSystem/v2-0203", "MB"), _configuration["FhirServer:System"] + "/fhir/memberidentifier", result.Member_ID));
            coverage.Identifier = coverageIdentifier;
            coverage.VersionId = coverage.VersionId; // TODO: CURRENLTY ACTIVE FOR ALL
            coverage.Meta = new Meta()
            {
                LastUpdated = DateTime.Now,
                Profile = new List<string>
                {
                    "http://hl7.org/fhir/us/carin-bb/StructureDefinition/C4BB-Coverage",
                    "http://hl7.org/fhir/us/carin-bb/StructureDefinition/C4BB-Coverage|1.0.0"
                }
            };
            coverage.Status = FinancialResourceStatusCodes.Active;
            coverage.Language = "en-US";
            coverage.Type = new CodeableConcept()
            {
                Coding = new List<Coding>
                {
                    new Coding("http://terminology.hl7.org/CodeSystem/v3-ActCode", "HIP")
                },
                Text = benefit.BenefitName,//TODO
            };
            coverage.Period = new Period { Start = result.Span.StartDate.ToString("dd/MM/yyyy"), End = result.Span.EndDate.ToString("dd/MM/yyyy") };
            coverage.Payor = new List<ResourceReference> { new ResourceReference("Organization/" + business.Id) };
            coverage.Class = new List<Coverage.ClassComponent>()
            {
                new Coverage.ClassComponent()
                {
                    Type = new CodeableConcept()
                    {
                         Coding = new List<Coding>()
                         {
                             new Coding("http://terminology.hl7.org/CodeSystem/coverage-class","plan"),
                         },
                         Text = "Plan"
                    },
                    Value = benefit.BenefitCode,
                    Name =benefit.BenefitName
                }
            };
            coverage.PolicyHolder = new ResourceReference()
            {
                Reference = "Patient/" + mcpMember.Id
            };
            coverage.Subscriber = new ResourceReference()
            {
                Reference = "Patient/" + mcpMember.Id
            };
            coverage.SubscriberId = result.Subscriber_ID;
            coverage.Beneficiary = new ResourceReference()
            {
                Reference = "Patient/" + mcpMember.Id
            };
            coverage.Dependent = result.PersonCode;
            coverage.Relationship = new CodeableConcept()
            {
                Coding = new List<Coding>()
                {
                    new Coding()
                    {
                        System = "http://terminology.hl7.org/CodeSystem/subscriber-relationship",
                        Code ="self"
                    }
                }
            };

            return coverage;
        }
      

        private Identifier CoverageIdentityfier(Coding coding, string system, string value)
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

        private Identifier CoverageOtherIdentityfier(string system, OtherIdentifier otherIdentities)
        {
            if (otherIdentities == null)
                return null;
            return new Identifier
            {
                System = system + otherIdentities.IdentityType.Name,// _configuration["FhirServer:System"] + "/fhir/memberidentifier",
                Value = otherIdentities.Value,//result.Id
            };
        }

        //private FinancialResourceStatusCodes GetMemberGendar()
        //{
        //    switch (gender)
        //    {
        //        case Gender.Male:
        //            return FinancialResourceStatusCodes.Male;
        //        case Gender.Female:
        //            return FinancialResourceStatusCodes.Female;
        //        case Gender.Unknown:
        //            return FinancialResourceStatusCodes.Unknown;
        //        default:
        //            return FinancialResourceStatusCodes.Unknown;
        //    }
        //}
    }
}