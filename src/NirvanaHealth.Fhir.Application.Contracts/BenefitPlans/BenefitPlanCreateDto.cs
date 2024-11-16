using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace NirvanaHealth.Fhir.BenefitPlans
{
    public class BenefitPlanCreateDto
    {
        [Required]
        public int BenefitPlan_ID { get; set; }
        public string BenefitName { get; set; }
        public string BenefitCode { get; set; }
        public string Description { get; set; }
        public string VersionNbr { get; set; }
    }
}