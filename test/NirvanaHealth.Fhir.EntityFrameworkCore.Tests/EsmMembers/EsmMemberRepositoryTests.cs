using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using NirvanaHealth.Fhir.EsmMembers;
using NirvanaHealth.Fhir.EntityFrameworkCore;
using Xunit;

namespace NirvanaHealth.Fhir.EsmMembers
{
    public class EsmMemberRepositoryTests : FhirEntityFrameworkCoreTestBase
    {
        private readonly IEsmMemberRepository _esmMemberRepository;

        public EsmMemberRepositoryTests()
        {
            _esmMemberRepository = GetRequiredService<IEsmMemberRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _esmMemberRepository.GetListAsync(
                    lastName: "6a44d348ad70475b90",
                    firstName: "4751618e8180436e92be84532cff5ee4662305f0fe85462bb2f6d8b3575b93265efdadee2db1",
                    mIddleName: "ceba746c232d42a9b08f61a78bed51dc318e719844364779bf614b",
                    suffix: "771f217db5e445e3a97751834ac6dd4c6bcbb8792c484ccfb76e2e",
                    preFix: "04d1961f25c74b8182169532cb4b8ab62d723515d4f24a9dae615b0d02a6b20798d6a745685d4a4689b2ee79a0dffa52308",
                    gender: default,
                    race: default
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                //result.First().Id.ShouldBe(Guid.Parse("707dd627-c4a5-46ee-be75-d584f8036ee9"));
                result.First().Id.ShouldBe("707dd627-c4a5-46ee-be75-d584f8036ee9");
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _esmMemberRepository.GetCountAsync(
                    lastName: "78ebae843b5d4d65962abceb11da10fa531b9475a85e401ca67ae3d99a7d5c0fc9b5fca5ddb8482fa076217b1012",
                    firstName: "05042a6fdf0a4ba6a",
                    mIddleName: "b8e16b17b78342aab314143e174df1f1f002b93322604ec392b37cc5a710bd4f6d2a4616c14748ac",
                    suffix: "53c3b8f7c2954d15b36da83f54473eb1289bb822a9d74b9fb",
                    preFix: "904bd5954ca849d185d0a60c03ba",
                    gender: default,
                    race: default
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}