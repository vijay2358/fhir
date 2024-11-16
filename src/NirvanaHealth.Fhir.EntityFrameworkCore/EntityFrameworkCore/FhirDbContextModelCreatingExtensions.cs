using NirvanaHealth.Fhir.MbrEnrollDetails;
using NirvanaHealth.Fhir.BenefitPlans;
using NirvanaHealth.Fhir.Businesses;
using Volo.Abp.EntityFrameworkCore.Modeling;
using NirvanaHealth.Fhir.EsmMembers;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace NirvanaHealth.Fhir.EntityFrameworkCore;

public static class FhirDbContextModelCreatingExtensions
{
    public static void ConfigureFhir(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        /* Configure all entities here. Example:

        builder.Entity<Question>(b =>
        {
            //Configure table & schema name
            b.ToTable(FhirDbProperties.DbTablePrefix + "Questions", FhirDbProperties.DbSchema);

            b.ConfigureByConvention();

            //Properties
            b.Property(q => q.Title).IsRequired().HasMaxLength(QuestionConsts.MaxTitleLength);

            //Relations
            b.HasMany(question => question.Tags).WithOne().HasForeignKey(qt => qt.QuestionId);

            //Indexes
            b.HasIndex(q => q.CreationTime);
        });
        */
        if (builder.IsHostDatabase())
        {
            builder.Entity<EsmMember>(b =>
{
    b.ToTable(FhirDbProperties.DbTablePrefix + "EsmMembers", FhirDbProperties.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.MCPMember_ID).HasColumnName(nameof(EsmMember.MCPMember_ID)).IsRequired();
    b.Property(x => x.LastName).HasColumnName(nameof(EsmMember.LastName)).IsRequired();
    b.Property(x => x.FirstName).HasColumnName(nameof(EsmMember.FirstName)).IsRequired();
    b.Property(x => x.MIddleName).HasColumnName(nameof(EsmMember.MIddleName));
    b.Property(x => x.Suffix).HasColumnName(nameof(EsmMember.Suffix));
    b.Property(x => x.PreFix).HasColumnName(nameof(EsmMember.PreFix));
    b.Property(x => x.DOB).HasColumnName(nameof(EsmMember.DOB));
    b.Property(x => x.Gender).HasColumnName(nameof(EsmMember.Gender)).IsRequired();
    b.Property(x => x.DateofDeath).HasColumnName(nameof(EsmMember.DateofDeath));
    b.Property(x => x.Race).HasColumnName(nameof(EsmMember.Race));
});

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<Business>(b =>
{
    b.ToTable(FhirDbProperties.DbTablePrefix + "Businesses", FhirDbProperties.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.Business_ID).HasColumnName(nameof(Business.Business_ID)).IsRequired();
    b.Property(x => x.BusinessName).HasColumnName(nameof(Business.BusinessName)).IsRequired();
    b.Property(x => x.BusinessCode).HasColumnName(nameof(Business.BusinessCode));
    b.Property(x => x.BusinessCat).HasColumnName(nameof(Business.BusinessCat)).IsRequired();
    b.Property(x => x.TrackerCode).HasColumnName(nameof(Business.TrackerCode)).IsRequired();
    b.Property(x => x.DBA).HasColumnName(nameof(Business.DBA));
    b.Property(x => x.FEIN).HasColumnName(nameof(Business.FEIN));
});

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<BenefitPlan>(b =>
{
    b.ToTable(FhirDbProperties.DbTablePrefix + "BenefitPlans", FhirDbProperties.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.BenefitPlan_ID).HasColumnName(nameof(BenefitPlan.BenefitPlan_ID)).IsRequired();
    b.Property(x => x.BenefitName).HasColumnName(nameof(BenefitPlan.BenefitName));
    b.Property(x => x.BenefitCode).HasColumnName(nameof(BenefitPlan.BenefitCode));
    b.Property(x => x.Description).HasColumnName(nameof(BenefitPlan.Description));
    b.Property(x => x.VersionNbr).HasColumnName(nameof(BenefitPlan.VersionNbr));
});

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<MbrEnrollDetail>(b =>
{
    b.ToTable(FhirDbProperties.DbTablePrefix + "MbrEnrollDetails", FhirDbProperties.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.MbrEnrollDetail_ID).HasColumnName(nameof(MbrEnrollDetail.MbrEnrollDetail_ID)).IsRequired();
    b.Property(x => x.MCPMember_ID).HasColumnName(nameof(MbrEnrollDetail.MCPMember_ID)).IsRequired();
    b.Property(x => x.BenefitPlan_ID).HasColumnName(nameof(MbrEnrollDetail.BenefitPlan_ID));
    b.Property(x => x.Member_ID).HasColumnName(nameof(MbrEnrollDetail.Member_ID));
    b.Property(x => x.Subscriber_ID).HasColumnName(nameof(MbrEnrollDetail.Subscriber_ID));
    b.Property(x => x.PersonCode).HasColumnName(nameof(MbrEnrollDetail.PersonCode));
});

        }
    }
}