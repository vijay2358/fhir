using NirvanaHealth.Fhir.MbrEnrollDetails;
using NirvanaHealth.Fhir.BenefitPlans;
using NirvanaHealth.Fhir.Businesses;
using NirvanaHealth.Fhir.EsmMembers;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.Abp.MongoDB;
using Volo.Abp.Uow;

namespace NirvanaHealth.Fhir.MongoDB;

[DependsOn(
    typeof(FhirDomainModule),
    typeof(AbpMongoDbModule)
    )]
public class FhirMongoDbModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMongoDbContext<FhirMongoDbContext>(options =>
        {
            /* Add custom repositories here. Example:
             * options.AddRepository<Question, MongoQuestionRepository>();
             */
            options.AddRepository<EsmMember, MongoEsmMemberRepository>();
            options.AddRepository<Business, MongoBusinessRepository>();
            options.AddRepository<BenefitPlan, MongoBenefitPlanRepository>();
            options.AddRepository<MbrEnrollDetail, MbrEnrollDetails.MongoMbrEnrollDetailRepository>();

        });

    }
}