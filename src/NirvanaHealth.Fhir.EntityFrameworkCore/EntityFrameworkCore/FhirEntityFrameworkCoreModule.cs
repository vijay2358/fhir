using NirvanaHealth.Fhir.MbrEnrollDetails;
using NirvanaHealth.Fhir.BenefitPlans;
using NirvanaHealth.Fhir.Businesses;
using NirvanaHealth.Fhir.EsmMembers;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace NirvanaHealth.Fhir.EntityFrameworkCore;

[DependsOn(
    typeof(FhirDomainModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
public class FhirEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<FhirDbContext>(options =>
        {
            /* Add custom repositories here. Example:
             * options.AddRepository<Question, EfCoreQuestionRepository>();
             */
            options.AddRepository<EsmMember, EsmMembers.EfCoreEsmMemberRepository>();

            options.AddRepository<Business, Businesses.EfCoreBusinessRepository>();

            options.AddRepository<BenefitPlan, BenefitPlans.EfCoreBenefitPlanRepository>();

            options.AddRepository<MbrEnrollDetail, MbrEnrollDetails.EfCoreMbrEnrollDetailRepository>();

        });
    }
}