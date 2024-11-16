using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace NirvanaHealth.Fhir.MbrEnrollDetails
{
    public class MbrEnrollDetailsAppServiceTests : FhirApplicationTestBase
    {
        private readonly IMbrEnrollDetailsAppService _mbrEnrollDetailsAppService;
        private readonly IRepository<MbrEnrollDetail, string> _mbrEnrollDetailRepository;

        public MbrEnrollDetailsAppServiceTests()
        {
            _mbrEnrollDetailsAppService = GetRequiredService<IMbrEnrollDetailsAppService>();
            _mbrEnrollDetailRepository = GetRequiredService<IRepository<MbrEnrollDetail, string>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _mbrEnrollDetailsAppService.GetListAsync(new GetMbrEnrollDetailsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id =="1").ShouldBe(true);
            result.Items.Any(x => x.Id == "2").ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _mbrEnrollDetailsAppService.GetAsync("1");

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe("1");
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new MbrEnrollDetailCreateDto
            {
                MbrEnrollDetail_ID = 318715,
                MCPMember_ID = 469477812,
                BenefitPlan_ID = 672748563,
                Member_ID = "20b54a422dd9",
                Subscriber_ID = "925421613948418d8929178896ee94",
                PersonCode = "c62e41dd0a6342e8bfcfb2652a583423fa2068b7cb4141879cbc7d3943cab76c3a573259b28442abb83af4846fc54887c46"
            };

            // Act
            var serviceResult = await _mbrEnrollDetailsAppService.CreateAsync(input);

            // Assert
            var result = await _mbrEnrollDetailRepository.FindAsync(c => c.MbrEnrollDetail_ID == serviceResult.MbrEnrollDetail_ID);

            result.ShouldNotBe(null);
            result.MbrEnrollDetail_ID.ShouldBe(318715);
            result.MCPMember_ID.ShouldBe(469477812);
            result.BenefitPlan_ID.ShouldBe(672748563);
            result.Member_ID.ShouldBe("20b54a422dd9");
            result.Subscriber_ID.ShouldBe("925421613948418d8929178896ee94");
            result.PersonCode.ShouldBe("c62e41dd0a6342e8bfcfb2652a583423fa2068b7cb4141879cbc7d3943cab76c3a573259b28442abb83af4846fc54887c46");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new MbrEnrollDetailUpdateDto()
            {
                MbrEnrollDetail_ID = 983932255,
                MCPMember_ID = 2140616671,
                BenefitPlan_ID = 1699924543,
                Member_ID = "2caa746535da4eeb9338f9d03c0f7ed38a08973fc1744e77b9bd94e7bb",
                Subscriber_ID = "e3495d5d8d974ddba548c246b55c40375581902218674beeb00084d4029f",
                PersonCode = "cb2ea503988a4b938f6aa7d00328a748fc52c058699e48779c590161c4b6f18"
            };

            // Act
            var serviceResult = await _mbrEnrollDetailsAppService.UpdateAsync("1", input);

            // Assert
            var result = await _mbrEnrollDetailRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.MbrEnrollDetail_ID.ShouldBe(983932255);
            result.MCPMember_ID.ShouldBe(2140616671);
            result.BenefitPlan_ID.ShouldBe(1699924543);
            result.Member_ID.ShouldBe("2caa746535da4eeb9338f9d03c0f7ed38a08973fc1744e77b9bd94e7bb");
            result.Subscriber_ID.ShouldBe("e3495d5d8d974ddba548c246b55c40375581902218674beeb00084d4029f");
            result.PersonCode.ShouldBe("cb2ea503988a4b938f6aa7d00328a748fc52c058699e48779c590161c4b6f18");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _mbrEnrollDetailsAppService.DeleteAsync("1");

            // Assert
            var result = await _mbrEnrollDetailRepository.FindAsync(c => c.Id == "1");

            result.ShouldBeNull();
        }
    }
}