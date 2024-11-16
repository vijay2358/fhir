using NirvanaHealth.Fhir;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace NirvanaHealth.Fhir.EsmMembers
{
    public class EsmMember : IEntity<string>
    {
        public object[] GetKeys()
        {
            return new object[] { Id };
        }

        public string Id { get; set; }
        public virtual int MCPMember_ID { get; set; }

        [NotNull]
        public virtual string LastName { get; set; }

        [NotNull]
        public virtual string FirstName { get; set; }

        [CanBeNull]
        public virtual string MIddleName { get; set; }

        [CanBeNull]
        public virtual string Suffix { get; set; }

        [CanBeNull]
        public virtual string PreFix { get; set; }

        public virtual DateTime? DOB { get; set; }

        public virtual Gender Gender { get; set; }

        public virtual DateTime? DateofDeath { get; set; }

        public virtual Race? Race { get; set; }

        [CanBeNull]
        public virtual List<Addresses> Addresses { get; set; }

        [CanBeNull]
        public virtual List<OtherIdentifier> OtherIdentities { get; set; }

        public EsmMember()
        {

        }

        public EsmMember(int mCPMember_ID, string lastName, string firstName, string mIddleName, string suffix, string preFix, Gender gender, DateTime? dOB = null, DateTime? dateofDeath = null, Race? race = null)
        {
        
            Check.NotNull(lastName, nameof(lastName));
            Check.NotNull(firstName, nameof(firstName));
            MCPMember_ID = mCPMember_ID;
            LastName = lastName;
            FirstName = firstName;
            MIddleName = mIddleName;
            Suffix = suffix;
            PreFix = preFix;
            Gender = gender;
            DOB = dOB;
            DateofDeath = dateofDeath;
            Race = race;
        }

    }
}