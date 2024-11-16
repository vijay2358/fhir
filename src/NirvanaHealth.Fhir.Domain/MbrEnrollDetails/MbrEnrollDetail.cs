using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;
using NirvanaHealth.Fhir.BenefitPlans;

namespace NirvanaHealth.Fhir.MbrEnrollDetails
{
    public class MbrEnrollDetail : IEntity<string>
    {
        public object[] GetKeys()
        {
            return new object[] { Id };
        }

        public string Id { get; set; }
        public virtual int MbrEnrollDetail_ID { get; set; }

        public virtual int MCPMember_ID { get; set; }

        public virtual int? BenefitPlan_ID { get; set; }

        [CanBeNull]
        public virtual string Member_ID { get; set; }

        [CanBeNull]
        public virtual string Subscriber_ID { get; set; }

        [CanBeNull]
        public virtual string PersonCode { get; set; }

        [CanBeNull]
        public virtual Span Span { get; set; }

        [CanBeNull]
        public virtual Relationship Relationship { get; set; }

        public MbrEnrollDetail()
        {

        }

        public MbrEnrollDetail(int mbrEnrollDetail_ID, int mCPMember_ID, string member_ID, string subscriber_ID, string personCode, int? benefitPlan_ID = null)
        {

            MbrEnrollDetail_ID = mbrEnrollDetail_ID;
            MCPMember_ID = mCPMember_ID;
            Member_ID = member_ID;
            Subscriber_ID = subscriber_ID;
            PersonCode = personCode;
            BenefitPlan_ID = benefitPlan_ID;
        }

    }
}