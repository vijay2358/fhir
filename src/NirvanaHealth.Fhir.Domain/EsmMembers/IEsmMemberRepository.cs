using NirvanaHealth.Fhir;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace NirvanaHealth.Fhir.EsmMembers
{
    public interface IEsmMemberRepository : IRepository<EsmMember, string>
    {
        Task<List<EsmMember>> GetListAsync(
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
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
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
            CancellationToken cancellationToken = default);

        Task<EsmMember> GetMemberByIdAsync(int mcpMemberId, CancellationToken cancellationToken = default(CancellationToken));

    }
}