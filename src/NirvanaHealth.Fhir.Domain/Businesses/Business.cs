using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace NirvanaHealth.Fhir.Businesses
{
    public class Business : IEntity<string>
    {

        public object[] GetKeys()
        {
            return new object[] { Id };
        }

        public string Id { get; set; }
        public virtual int Business_ID { get; set; }

        [NotNull]
        public virtual string BusinessName { get; set; }

        [CanBeNull]
        public virtual string BusinessCode { get; set; }

        public virtual int? BusinessCat { get; set; }

        [NotNull]
        public virtual string TrackerCode { get; set; }

        [CanBeNull]
        public virtual string DBA { get; set; }

        [CanBeNull]
        public virtual string FEIN { get; set; }

        [CanBeNull]
        public virtual List<Addresses> Addresses { get; set; }

        public Business()
        {

        }

        public Business(int business_ID, string businessName, string businessCode, string trackerCode, string dBA, string fEIN, int? businessCat = null)
        {

            Check.NotNull(businessName, nameof(businessName));
            Check.NotNull(trackerCode, nameof(trackerCode));
            Business_ID = business_ID;
            BusinessName = businessName;
            BusinessCode = businessCode;
            TrackerCode = trackerCode;
            DBA = dBA;
            FEIN = fEIN;
            BusinessCat = businessCat;
        }

    }
}