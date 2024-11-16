using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace NirvanaHealth.Fhir.BenefitPlans
{
    public class BenefitPlansAppServiceTests : FhirApplicationTestBase
    {
        private readonly IBenefitPlansAppService _benefitPlansAppService;
        private readonly IRepository<BenefitPlan, string> _benefitPlanRepository;

        public BenefitPlansAppServiceTests()
        {
            _benefitPlansAppService = GetRequiredService<IBenefitPlansAppService>();
            _benefitPlanRepository = GetRequiredService<IRepository<BenefitPlan, string>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _benefitPlansAppService.GetListAsync(new GetBenefitPlansInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == "1").ShouldBe(true);
            result.Items.Any(x => x.Id == "2").ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _benefitPlansAppService.GetAsync("1");

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe("1");
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new BenefitPlanCreateDto
            {
                BenefitPlan_ID = 864124084,
                BenefitName = "53070703055b48a2b5da0e041f5",
                BenefitCode = "5a2c9f64cf574e7da8207",
                Description = "d8c0b1d6127e4c499196ed02863f71b1a65bb8bcde3045c8",
                VersionNbr = "9a5a241322464b08baa16c92516a92d8121e9f3c444642d4a026a152abe0e9a"
            };

            // Act
            var serviceResult = await _benefitPlansAppService.CreateAsync(input);

            // Assert
            var result = await _benefitPlanRepository.FindAsync(c => c.BenefitPlan_ID == serviceResult.BenefitPlan_ID);

            result.ShouldNotBe(null);
            result.BenefitPlan_ID.ShouldBe(864124084);
            result.BenefitName.ShouldBe("53070703055b48a2b5da0e041f5");
            result.BenefitCode.ShouldBe("5a2c9f64cf574e7da8207");
            result.Description.ShouldBe("d8c0b1d6127e4c499196ed02863f71b1a65bb8bcde3045c8");
            result.VersionNbr.ShouldBe("9a5a241322464b08baa16c92516a92d8121e9f3c444642d4a026a152abe0e9a");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new BenefitPlanUpdateDto()
            {
                BenefitPlan_ID = 794747143,
                BenefitName = "7691d7146518465b9ed4bb84d12dd9606ccd18529f754a95a9fd8c8afd198fdea8a4af259f04482cbb973aa87634",
                BenefitCode = "c4ce62be48fa4746bd6d67763111d9583c3cca8d009a4571b",
                Description = "02fce44f0e994601950a9df401b",
                VersionNbr = "db3da0f9122b464bb8a2f3fcae6f272db9269e91f10d434cb"
            };

            // Act
            var serviceResult = await _benefitPlansAppService.UpdateAsync("1", input);

            // Assert
            var result = await _benefitPlanRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.BenefitPlan_ID.ShouldBe(794747143);
            result.BenefitName.ShouldBe("7691d7146518465b9ed4bb84d12dd9606ccd18529f754a95a9fd8c8afd198fdea8a4af259f04482cbb973aa87634");
            result.BenefitCode.ShouldBe("c4ce62be48fa4746bd6d67763111d9583c3cca8d009a4571b");
            result.Description.ShouldBe("02fce44f0e994601950a9df401b");
            result.VersionNbr.ShouldBe("db3da0f9122b464bb8a2f3fcae6f272db9269e91f10d434cb");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _benefitPlansAppService.DeleteAsync("1");

            // Assert
            var result = await _benefitPlanRepository.FindAsync(c => c.Id == "1");

            result.ShouldBeNull();
        }
    }
}