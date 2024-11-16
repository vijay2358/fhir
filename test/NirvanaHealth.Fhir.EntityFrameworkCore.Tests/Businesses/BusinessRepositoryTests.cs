using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using NirvanaHealth.Fhir.Businesses;
using NirvanaHealth.Fhir.EntityFrameworkCore;
using Xunit;

namespace NirvanaHealth.Fhir.Businesses
{
    public class BusinessRepositoryTests : FhirEntityFrameworkCoreTestBase
    {
        private readonly IBusinessRepository _businessRepository;

        public BusinessRepositoryTests()
        {
            _businessRepository = GetRequiredService<IBusinessRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _businessRepository.GetListAsync(
                    businessName: "f3012f816b694246aafe69da534b45df96944",
                    businessCode: "7e4af17504474840995c77237488f2a099d494c099014a6",
                    trackerCode: "27d5bd4d80a0472eb75b931b3f2ab4f57937f6d1460247559f9b8558",
                    dBA: "1888d38c45bc427d85a511593e675b019672d61f282449f69081299dcd2cbacc78d003865e2f4fe28e68587ffd",
                    fEIN: "ea16b9abef774681bb770a4f4fc2bd0"
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
                var result = await _businessRepository.GetCountAsync(
                    businessName: "4be7ddb7f7d34090bf73ef2cd92431",
                    businessCode: "82e2510c5a1043d5be4e7e0902f408220bf87f8e36504894820dd438726150464fabc05091b24f5ca0",
                    trackerCode: "1ff85ba06d9e44c7826fee5058b61cdbb0543e9858e94ddab6391d916d427b618d491a0cb4f1423d9fd2c8811328a5b052",
                    dBA: "2027ea506d814",
                    fEIN: "1df823e8b691497ab23f88c19a3e6cc5af721de81eb84f7eb90e9c82d6352aa90f9758d9c9ed4b7b8"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}