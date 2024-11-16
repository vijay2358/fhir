using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using NirvanaHealth.Fhir.BenefitPlans;
using NirvanaHealth.Fhir.EntityFrameworkCore;
using Xunit;

namespace NirvanaHealth.Fhir.BenefitPlans
{
    public class BenefitPlanRepositoryTests : FhirEntityFrameworkCoreTestBase
    {
        private readonly IBenefitPlanRepository _benefitPlanRepository;

        public BenefitPlanRepositoryTests()
        {
            _benefitPlanRepository = GetRequiredService<IBenefitPlanRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _benefitPlanRepository.GetListAsync(
                    benefitName: "f0581349b3cc47bdbeb46d8529d60fda9b0b5ae6efdd44cb95e0a32104cb6",
                    benefitCode: "c3bcce8487a148988fbc37e35b583a2d27ac8579516c4bcba59a52bc639d6bfc0e1958f69a5841a1a5b",
                    description: "3163163cc6df482a80c5f0c6c19ba5e150a7fb71f19b461987d6ffc51d3df47041875851c47c4048",
                    versionNbr: "1ec313e204ba45c7b6713c52f50b0030460e1e2f808b40e49b"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe("1");
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _benefitPlanRepository.GetCountAsync(
                    benefitName: "4052b18054524",
                    benefitCode: "db8c91",
                    description: "8e76a570e56f4abab729806db31",
                    versionNbr: "cceef1e7c8af4eb1a93d10eb83f53d"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}