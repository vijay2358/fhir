using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace NirvanaHealth.Fhir.BenefitPlans
{
    public class BenefitPlan : IEntity<string>
    {
        public object[] GetKeys()
        {
            return new object[] { Id };
        }

        public string Id { get; set; }
        public virtual int BenefitPlan_ID { get; set; }

        [CanBeNull]
        public virtual string BenefitName { get; set; }

        [CanBeNull]
        public virtual string BenefitCode { get; set; }

        [CanBeNull]
        public virtual string Description { get; set; }

        [CanBeNull]
        public virtual string VersionNbr { get; set; }

        public virtual Span Span { get; set; }

        public virtual BusinessLink Business { get; set; }

        public BenefitPlan()
        {

        }

        public BenefitPlan(int benefitPlan_ID, string benefitName, string benefitCode, string description, string versionNbr)
        {

            BenefitPlan_ID = benefitPlan_ID;
            BenefitName = benefitName;
            BenefitCode = benefitCode;
            Description = description;
            VersionNbr = versionNbr;
        }

    }

    public class Span
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }

    public class BusinessLink
    {
        public int Business_ID { get; set; }
        public string BusinessName { get; set; }

    }

}