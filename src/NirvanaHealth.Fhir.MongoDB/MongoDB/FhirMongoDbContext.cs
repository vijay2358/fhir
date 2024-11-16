using NirvanaHealth.Fhir.MbrEnrollDetails;
using NirvanaHealth.Fhir.BenefitPlans;
using NirvanaHealth.Fhir.Businesses;
using NirvanaHealth.Fhir.EsmMembers;
using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson;

namespace NirvanaHealth.Fhir.MongoDB;

[ConnectionStringName(FhirDbProperties.ConnectionStringName)]
public class FhirMongoDbContext : AbpMongoDbContext, IFhirMongoDbContext
{
    public IMongoCollection<MbrEnrollDetail> MbrEnrollDetails => Collection<MbrEnrollDetail>();
    public IMongoCollection<BenefitPlan> BenefitPlans => Collection<BenefitPlan>();
    public IMongoCollection<Business> Businesses => Collection<Business>();
    public IMongoCollection<EsmMember> EsmMembers => Collection<EsmMember>();
    /* Add mongo collections here. Example:
     * public IMongoCollection<Question> Questions => Collection<Question>();
     */

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);
        modelBuilder.Entity<Business>(b =>
        {

            b.CollectionName = FhirDbProperties.DbTablePrefix + "Business";
            b.BsonMap.AutoMap();
            b.BsonMap.SetIgnoreExtraElements(true);
            b.BsonMap.MapIdProperty(x => x.Id).SetIdGenerator(StringObjectIdGenerator.Instance)
              .SetSerializer(new StringSerializer()
              .WithRepresentation(BsonType.ObjectId));

        });

        modelBuilder.Entity<BenefitPlan>(b =>
        {

            b.CollectionName = FhirDbProperties.DbTablePrefix + "BenefitPlan";
            b.BsonMap.AutoMap();
            b.BsonMap.SetIgnoreExtraElements(true);
            b.BsonMap.MapIdProperty(x => x.Id).SetIdGenerator(StringObjectIdGenerator.Instance)
              .SetSerializer(new StringSerializer()
              .WithRepresentation(BsonType.ObjectId));

        });

        modelBuilder.Entity<EsmMember>(b =>
        {

            b.CollectionName = FhirDbProperties.DbTablePrefix + "MCPMember";
            b.BsonMap.AutoMap();
            b.BsonMap.SetIgnoreExtraElements(true);
            b.BsonMap.MapIdProperty(x => x.Id).SetIdGenerator(StringObjectIdGenerator.Instance)
              .SetSerializer(new StringSerializer()
              .WithRepresentation(BsonType.ObjectId));

        });

        modelBuilder.Entity<MbrEnrollDetail>(b =>
        {

            b.CollectionName = FhirDbProperties.DbTablePrefix + "MbrEnrollDetails";
            b.BsonMap.AutoMap();
            b.BsonMap.SetIgnoreExtraElements(true);
            b.BsonMap.MapIdProperty(x => x.Id).SetIdGenerator(StringObjectIdGenerator.Instance)
              .SetSerializer(new StringSerializer()
              .WithRepresentation(BsonType.ObjectId));

        });

        modelBuilder.ConfigureFhir();

        //modelBuilder.Entity<EsmMember>(b => { b.CollectionName = FhirDbProperties.DbTablePrefix + "EsmMembers"; });

        //modelBuilder.Entity<Business>(b => { b.CollectionName = FhirDbProperties.DbTablePrefix + "Businesses"; });

        //modelBuilder.Entity<BenefitPlan>(b => { b.CollectionName = FhirDbProperties.DbTablePrefix + "BenefitPlans"; });

        //modelBuilder.Entity<MbrEnrollDetail>(b => { b.CollectionName = FhirDbProperties.DbTablePrefix + "MbrEnrollDetails"; });
    }
}