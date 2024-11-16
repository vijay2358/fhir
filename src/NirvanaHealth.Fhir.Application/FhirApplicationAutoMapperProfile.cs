using NirvanaHealth.Fhir.MbrEnrollDetails;
using NirvanaHealth.Fhir.BenefitPlans;
using NirvanaHealth.Fhir.Businesses;
using System;
using NirvanaHealth.Fhir.Shared;
using Volo.Abp.AutoMapper;
using NirvanaHealth.Fhir.EsmMembers;
using AutoMapper;

namespace NirvanaHealth.Fhir;

public class FhirApplicationAutoMapperProfile : Profile
{
    public FhirApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<EsmMember, EsmMemberDto>();

        CreateMap<Business, BusinessDto>();
        CreateMap<Addresses, AddressesDto>();
        CreateMap<AddressType, AddressTypeDto>();

        CreateMap<BenefitPlan, BenefitPlanDto>();

        CreateMap<MbrEnrollDetail, MbrEnrollDetailDto>();
    }
}