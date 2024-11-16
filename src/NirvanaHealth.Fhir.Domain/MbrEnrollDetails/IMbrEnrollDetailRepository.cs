using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace NirvanaHealth.Fhir.MbrEnrollDetails
{
    public interface IMbrEnrollDetailRepository : IRepository<MbrEnrollDetail, string>
    {
        Task<List<MbrEnrollDetail>> GetListAsync(
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
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
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
            CancellationToken cancellationToken = default);

        Task<MbrEnrollDetail> GetMbrEnrollDetailByIdAsync(int mbrEnrollmentId, CancellationToken cancellationToken = default(CancellationToken));

    }
}