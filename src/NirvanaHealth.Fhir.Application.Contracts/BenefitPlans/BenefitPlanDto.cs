using System;
using Volo.Abp.Application.Dtos;

namespace NirvanaHealth.Fhir.BenefitPlans
{
    public class BenefitPlanDto : EntityDto<string>
    {
        public int BenefitPlan_ID { get; set; }
        public string BenefitName { get; set; }
        public string BenefitCode { get; set; }
        public string Description { get; set; }
        public string VersionNbr { get; set; }
    }
}