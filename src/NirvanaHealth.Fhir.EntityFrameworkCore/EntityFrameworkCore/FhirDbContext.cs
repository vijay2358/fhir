using NirvanaHealth.Fhir.MbrEnrollDetails;
using NirvanaHealth.Fhir.BenefitPlans;
using NirvanaHealth.Fhir.Businesses;
using NirvanaHealth.Fhir.EsmMembers;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace NirvanaHealth.Fhir.EntityFrameworkCore;

[ConnectionStringName(FhirDbProperties.ConnectionStringName)]
public class FhirDbContext : AbpDbContext<FhirDbContext>, IFhirDbContext
{
    public DbSet<MbrEnrollDetail> MbrEnrollDetails { get; set; }
    public DbSet<BenefitPlan> BenefitPlans { get; set; }
    public DbSet<Business> Businesses { get; set; }
    public DbSet<EsmMember> EsmMembers { get; set; }
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */

    public FhirDbContext(DbContextOptions<FhirDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureFhir();
    }
}