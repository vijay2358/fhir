using NirvanaHealth.Fhir.MbrEnrollDetails;
using NirvanaHealth.Fhir.BenefitPlans;
using NirvanaHealth.Fhir.Businesses;
using NirvanaHealth.Fhir.EsmMembers;
using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace NirvanaHealth.Fhir.MongoDB;

[ConnectionStringName(FhirDbProperties.ConnectionStringName)]
public interface IFhirMongoDbContext : IAbpMongoDbContext
{
    IMongoCollection<MbrEnrollDetail> MbrEnrollDetails { get; }
    IMongoCollection<BenefitPlan> BenefitPlans { get; }
    IMongoCollection<Business> Businesses { get; }
    IMongoCollection<EsmMember> EsmMembers { get; }
    /* Define mongo collections here. Example:
     * IMongoCollection<Question> Questions { get; }
     */
}