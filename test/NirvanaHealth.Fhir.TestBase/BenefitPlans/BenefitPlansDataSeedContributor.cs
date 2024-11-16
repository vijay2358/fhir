using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using NirvanaHealth.Fhir.BenefitPlans;

namespace NirvanaHealth.Fhir.BenefitPlans
{
    public class BenefitPlansDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IBenefitPlanRepository _benefitPlanRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public BenefitPlansDataSeedContributor(IBenefitPlanRepository benefitPlanRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _benefitPlanRepository = benefitPlanRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _benefitPlanRepository.InsertAsync(new BenefitPlan
            (
                benefitPlan_ID: 1470332788,
                benefitName: "f0581349b3cc47bdbeb46d8529d60fda9b0b5ae6efdd44cb95e0a32104cb6",
                benefitCode: "c3bcce8487a148988fbc37e35b583a2d27ac8579516c4bcba59a52bc639d6bfc0e1958f69a5841a1a5b",
                description: "3163163cc6df482a80c5f0c6c19ba5e150a7fb71f19b461987d6ffc51d3df47041875851c47c4048",
                versionNbr: "1ec313e204ba45c7b6713c52f50b0030460e1e2f808b40e49b"
            ));

            await _benefitPlanRepository.InsertAsync(new BenefitPlan
            (
                benefitPlan_ID: 1170224632,
                benefitName: "4052b18054524",
                benefitCode: "db8c91",
                description: "8e76a570e56f4abab729806db31",
                versionNbr: "cceef1e7c8af4eb1a93d10eb83f53d"
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}