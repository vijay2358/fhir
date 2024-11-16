using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using NirvanaHealth.Fhir.MbrEnrollDetails;

namespace NirvanaHealth.Fhir.MbrEnrollDetails
{
    public class MbrEnrollDetailsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IMbrEnrollDetailRepository _mbrEnrollDetailRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public MbrEnrollDetailsDataSeedContributor(IMbrEnrollDetailRepository mbrEnrollDetailRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _mbrEnrollDetailRepository = mbrEnrollDetailRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _mbrEnrollDetailRepository.InsertAsync(new MbrEnrollDetail
            (
                mbrEnrollDetail_ID: 2030921203,
                mCPMember_ID: 924826378,
                benefitPlan_ID: 1672386360,
                member_ID: "0196cce25823",
                subscriber_ID: "3cf7c53291084bf1a1d2be9981f83bda328b1cf",
                personCode: "16b906504"
            ));

            await _mbrEnrollDetailRepository.InsertAsync(new MbrEnrollDetail
            (
                mbrEnrollDetail_ID: 1533044968,
                mCPMember_ID: 1194312122,
                benefitPlan_ID: 579089589,
                member_ID: "140f3862a2a240ae8a2da8902c9216a3a0bd033",
                subscriber_ID: "6bdcc71811c54cfbaeaad33c216b9d6e2ea22b381f6f446db",
                personCode: "6956dfe546f24c94b"
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}