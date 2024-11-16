using NirvanaHealth.Fhir;
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

namespace NirvanaHealth.Fhir.EsmMembers
{
    public class MongoEsmMemberRepository : MongoDbRepository<FhirMongoDbContext, EsmMember, string>, IEsmMemberRepository
    {
        public MongoEsmMemberRepository(IMongoDbContextProvider<FhirMongoDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<List<EsmMember>> GetListAsync(
            string filterText = null,
            int? mCPMember_IDMin = null,
            int? mCPMember_IDMax = null,
            string lastName = null,
            string firstName = null,
            string mIddleName = null,
            string suffix = null,
            string preFix = null,
            DateTime? dOBMin = null,
            DateTime? dOBMax = null,
            Gender? gender = null,
            DateTime? dateofDeathMin = null,
            DateTime? dateofDeathMax = null,
            Race? race = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetMongoQueryableAsync(cancellationToken)), filterText, mCPMember_IDMin, mCPMember_IDMax, lastName, firstName, mIddleName, suffix, preFix, dOBMin, dOBMax, gender, dateofDeathMin, dateofDeathMax, race);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? EsmMemberConsts.GetDefaultSorting(false) : sorting);
            return await query.As<IMongoQueryable<EsmMember>>()
                .PageBy<EsmMember, IMongoQueryable<EsmMember>>(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<long> GetCountAsync(
           string filterText = null,
           int? mCPMember_IDMin = null,
           int? mCPMember_IDMax = null,
           string lastName = null,
           string firstName = null,
           string mIddleName = null,
           string suffix = null,
           string preFix = null,
           DateTime? dOBMin = null,
           DateTime? dOBMax = null,
           Gender? gender = null,
           DateTime? dateofDeathMin = null,
           DateTime? dateofDeathMax = null,
           Race? race = null,
           CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetMongoQueryableAsync(cancellationToken)), filterText, mCPMember_IDMin, mCPMember_IDMax, lastName, firstName, mIddleName, suffix, preFix, dOBMin, dOBMax, gender, dateofDeathMin, dateofDeathMax, race);
            return await query.As<IMongoQueryable<EsmMember>>().LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<EsmMember> ApplyFilter(
            IQueryable<EsmMember> query,
            string filterText,
            int? mCPMember_IDMin = null,
            int? mCPMember_IDMax = null,
            string lastName = null,
            string firstName = null,
            string mIddleName = null,
            string suffix = null,
            string preFix = null,
            DateTime? dOBMin = null,
            DateTime? dOBMax = null,
            Gender? gender = null,
            DateTime? dateofDeathMin = null,
            DateTime? dateofDeathMax = null,
            Race? race = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.LastName.Contains(filterText) || e.FirstName.Contains(filterText) || e.MIddleName.Contains(filterText) || e.Suffix.Contains(filterText) || e.PreFix.Contains(filterText))
                    .WhereIf(mCPMember_IDMin.HasValue, e => e.MCPMember_ID >= mCPMember_IDMin.Value)
                    .WhereIf(mCPMember_IDMax.HasValue, e => e.MCPMember_ID <= mCPMember_IDMax.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(lastName), e => e.LastName.Contains(lastName))
                    .WhereIf(!string.IsNullOrWhiteSpace(firstName), e => e.FirstName.Contains(firstName))
                    .WhereIf(!string.IsNullOrWhiteSpace(mIddleName), e => e.MIddleName.Contains(mIddleName))
                    .WhereIf(!string.IsNullOrWhiteSpace(suffix), e => e.Suffix.Contains(suffix))
                    .WhereIf(!string.IsNullOrWhiteSpace(preFix), e => e.PreFix.Contains(preFix))
                    .WhereIf(dOBMin.HasValue, e => e.DOB >= dOBMin.Value)
                    .WhereIf(dOBMax.HasValue, e => e.DOB <= dOBMax.Value)
                    .WhereIf(gender.HasValue, e => e.Gender == gender)
                    .WhereIf(dateofDeathMin.HasValue, e => e.DateofDeath >= dateofDeathMin.Value)
                    .WhereIf(dateofDeathMax.HasValue, e => e.DateofDeath <= dateofDeathMax.Value)
                    .WhereIf(race.HasValue, e => e.Race == race);
        }

        public async Task<EsmMember> GetMemberByIdAsync(int mcpMemberId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = (await GetMongoQueryableAsync())
                    .Where(x => x.MCPMember_ID == mcpMemberId);
            return await result.FirstOrDefaultAsync();
        }
    }
}