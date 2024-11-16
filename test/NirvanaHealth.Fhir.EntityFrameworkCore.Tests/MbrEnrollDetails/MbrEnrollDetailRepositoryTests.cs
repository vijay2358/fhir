using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using NirvanaHealth.Fhir.MbrEnrollDetails;
using NirvanaHealth.Fhir.EntityFrameworkCore;
using Xunit;

namespace NirvanaHealth.Fhir.MbrEnrollDetails
{
    public class MbrEnrollDetailRepositoryTests : FhirEntityFrameworkCoreTestBase
    {
        private readonly IMbrEnrollDetailRepository _mbrEnrollDetailRepository;

        public MbrEnrollDetailRepositoryTests()
        {
            _mbrEnrollDetailRepository = GetRequiredService<IMbrEnrollDetailRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _mbrEnrollDetailRepository.GetListAsync(
                    member_ID: "0196cce25823",
                    subscriber_ID: "3cf7c53291084bf1a1d2be9981f83bda328b1cf",
                    personCode: "16b906504"
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
                var result = await _mbrEnrollDetailRepository.GetCountAsync(
                    member_ID: "140f3862a2a240ae8a2da8902c9216a3a0bd033",
                    subscriber_ID: "6bdcc71811c54cfbaeaad33c216b9d6e2ea22b381f6f446db",
                    personCode: "6956dfe546f24c94b"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}