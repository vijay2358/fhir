using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hl7.Fhir.Model;
using JetBrains.Annotations;
using NirvanaHealth.Fhir.Businesses;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace NirvanaHealth.Fhir.BenefitPlans
{
    public class BenefitPlanManager : DomainService
    {
        private readonly IBenefitPlanRepository _benefitPlanRepository;
        private readonly CommonDomainManager _commonDomainManager;

        public BenefitPlanManager(IBenefitPlanRepository benefitPlanRepository, CommonDomainManager commonDomainManager)
        {
            _benefitPlanRepository = benefitPlanRepository;
            _commonDomainManager = commonDomainManager;
        }

        public async Task<Coverage> MapOrganization(BenefitPlan result, string Id)
        {
            var coverage = new Coverage()
            {
                Id = result.Id
            };
            var coverageIdentifier = new List<Hl7.Fhir.Model.Identifier>();
            //System ID 
            coverageIdentifier.Add(_commonDomainManager.GetIdentifier(result.Id));
            coverage.Identifier = coverageIdentifier;
            coverage.VersionId = coverage.VersionId; // TODO: CURRENLTY ACTIVE FOR ALL
            coverage.Type = new CodeableConcept()
            {
                Coding = new List<Coding>
                {
                    new Coding("http://terminology.hl7.org/CodeSystem/v3-ActCode", "HIP")
                },
                Text = result.BenefitName//TODO
            };
            coverage.Period = new Period { Start = result.Span.StartDate.ToString(), End = result.Span.EndDate.ToString() };
            coverage.Payor = new List<ResourceReference> { new ResourceReference("Organization/" + Id) };
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
                    Value = result.BenefitCode,
                    Name =result.BenefitName
                }
            };
            return coverage;
        }
        public async Task<BenefitPlan> CreateAsync(
        int benefitPlan_ID, string benefitName, string benefitCode, string description, string versionNbr)
        {
            var benefitPlan = new BenefitPlan(

             benefitPlan_ID, benefitName, benefitCode, description, versionNbr
             );

            return await _benefitPlanRepository.InsertAsync(benefitPlan);
        }

        public async Task<BenefitPlan> UpdateAsync(
            string id,
            int benefitPlan_ID, string benefitName, string benefitCode, string description, string versionNbr
        )
        {
            var queryable = await _benefitPlanRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var benefitPlan = await AsyncExecuter.FirstOrDefaultAsync(query);

            benefitPlan.BenefitPlan_ID = benefitPlan_ID;
            benefitPlan.BenefitName = benefitName;
            benefitPlan.BenefitCode = benefitCode;
            benefitPlan.Description = description;
            benefitPlan.VersionNbr = versionNbr;

            return await _benefitPlanRepository.UpdateAsync(benefitPlan);
        }

    }
}