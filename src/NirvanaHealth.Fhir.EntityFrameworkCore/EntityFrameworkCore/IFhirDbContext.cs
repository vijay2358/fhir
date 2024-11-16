using NirvanaHealth.Fhir.MbrEnrollDetails;
using NirvanaHealth.Fhir.BenefitPlans;
using NirvanaHealth.Fhir.Businesses;
using NirvanaHealth.Fhir.EsmMembers;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace NirvanaHealth.Fhir.EntityFrameworkCore;

[ConnectionStringName(FhirDbProperties.ConnectionStringName)]
public interface IFhirDbContext : IEfCoreDbContext
{
    DbSet<MbrEnrollDetail> MbrEnrollDetails { get; set; }
    DbSet<BenefitPlan> BenefitPlans { get; set; }
    DbSet<Business> Businesses { get; set; }
    DbSet<EsmMember> EsmMembers { get; set; }
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
}