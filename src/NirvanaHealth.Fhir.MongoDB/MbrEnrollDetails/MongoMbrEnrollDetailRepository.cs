using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using NirvanaHealth.Fhir.MongoDB;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;
using MongoDB.Driver.Linq;
using MongoDB.Driver;

namespace NirvanaHealth.Fhir.MbrEnrollDetails
{
    public class MongoMbrEnrollDetailRepository : MongoDbRepository<FhirMongoDbContext, MbrEnrollDetail, string>, IMbrEnrollDetailRepository
    {
        public MongoMbrEnrollDetailRepository(IMongoDbContextProvider<FhirMongoDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<List<MbrEnrollDetail>> GetListAsync(
            string filterText = null,
            int? mbrEnrollDetail_IDMin = null,
            int? mbrEnrollDetail_IDMax = null,
            int? mCPMember_IDMin = null,
            int? mCPMember_IDMax = null,
            int? benefitPlan_IDMin = null,
            int? benefitPlan_IDMax = null,
            string member_ID = null,
            string subscriber_ID = null,
            string personCode = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetMongoQueryableAsync(cancellationToken)), filterText, mbrEnrollDetail_IDMin, mbrEnrollDetail_IDMax, mCPMember_IDMin, mCPMember_IDMax, benefitPlan_IDMin, benefitPlan_IDMax, member_ID, subscriber_ID, personCode);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? MbrEnrollDetailConsts.GetDefaultSorting(false) : sorting);
            return await query.As<IMongoQueryable<MbrEnrollDetail>>()
                .PageBy<MbrEnrollDetail, IMongoQueryable<MbrEnrollDetail>>(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<long> GetCountAsync(
           string filterText = null,
           int? mbrEnrollDetail_IDMin = null,
           int? mbrEnrollDetail_IDMax = null,
           int? mCPMember_IDMin = null,
           int? mCPMember_IDMax = null,
           int? benefitPlan_IDMin = null,
           int? benefitPlan_IDMax = null,
           string member_ID = null,
           string subscriber_ID = null,
           string personCode = null,
           CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetMongoQueryableAsync(cancellationToken)), filterText, mbrEnrollDetail_IDMin, mbrEnrollDetail_IDMax, mCPMember_IDMin, mCPMember_IDMax, benefitPlan_IDMin, benefitPlan_IDMax, member_ID, subscriber_ID, personCode);
            return await query.As<IMongoQueryable<MbrEnrollDetail>>().LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<MbrEnrollDetail> ApplyFilter(
            IQueryable<MbrEnrollDetail> query,
            string filterText,
            int? mbrEnrollDetail_IDMin = null,
            int? mbrEnrollDetail_IDMax = null,
            int? mCPMember_IDMin = null,
            int? mCPMember_IDMax = null,
            int? benefitPlan_IDMin = null,
            int? benefitPlan_IDMax = null,
            string member_ID = null,
            string subscriber_ID = null,
            string personCode = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Member_ID.Contains(filterText) || e.Subscriber_ID.Contains(filterText) || e.PersonCode.Contains(filterText))
                    .WhereIf(mbrEnrollDetail_IDMin.HasValue, e => e.MbrEnrollDetail_ID >= mbrEnrollDetail_IDMin.Value)
                    .WhereIf(mbrEnrollDetail_IDMax.HasValue, e => e.MbrEnrollDetail_ID <= mbrEnrollDetail_IDMax.Value)
                    .WhereIf(mCPMember_IDMin.HasValue, e => e.MCPMember_ID >= mCPMember_IDMin.Value)
                    .WhereIf(mCPMember_IDMax.HasValue, e => e.MCPMember_ID <= mCPMember_IDMax.Value)
                    .WhereIf(benefitPlan_IDMin.HasValue, e => e.BenefitPlan_ID >= benefitPlan_IDMin.Value)
                    .WhereIf(benefitPlan_IDMax.HasValue, e => e.BenefitPlan_ID <= benefitPlan_IDMax.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(member_ID), e => e.Member_ID.Contains(member_ID))
                    .WhereIf(!string.IsNullOrWhiteSpace(subscriber_ID), e => e.Subscriber_ID.Contains(subscriber_ID))
                    .WhereIf(!string.IsNullOrWhiteSpace(personCode), e => e.PersonCode.Contains(personCode));
        }

        public async Task<MbrEnrollDetail> GetMbrEnrollDetailByIdAsync(int mbrEnrollmentId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = (await GetMongoQueryableAsync())
                    .Where(x => x.MbrEnrollDetail_ID == mbrEnrollmentId);
            return await result.FirstOrDefaultAsync();
        }
    }
}