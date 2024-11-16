using System;
using System.Collections.Generic;
using System.Text;

namespace NirvanaHealth.Fhir
{
    public class Addresses
    {
        
           
            public virtual int Address_ID { get; set; }
            public virtual AddressType Address_Type { get; set; }
            public virtual string Address1 { get; set; }
            public virtual string Address2 { get; set; }
            public virtual string City { get; set; }
            public virtual string State { get; set; }
            public virtual string Zip { get; set; }
            public virtual string Country { get; set; }
        
    }

    public class AddressType
    {
       
        public virtual int Type_ID { get; set; }
        public virtual string Name { get; set; }
    }

    public class IdentityType
    {

        public virtual int IdentityType_ID { get; set; }
        public virtual string Name { get; set; }
    }

    public class OtherIdentifier
    {
        public virtual int OtherIdentitiesID { get; set; }
        public virtual IdentityType IdentityType { get; set; }
        public virtual string Value { get; set; }
    }
}
