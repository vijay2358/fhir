using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace NirvanaHealth.Fhir.Businesses
{
    public class BusinessesAppServiceTests : FhirApplicationTestBase
    {
        private readonly IBusinessesAppService _businessesAppService;
        private readonly IRepository<Business, string> _businessRepository;

        public BusinessesAppServiceTests()
        {
            _businessesAppService = GetRequiredService<IBusinessesAppService>();
            _businessRepository = GetRequiredService<IRepository<Business, string>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _businessesAppService.GetListAsync(new GetBusinessesInput());

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
            var result = await _businessesAppService.GetAsync("1");

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe("1");
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new BusinessCreateDto
            {
                Business_ID = 1480712846,
                BusinessName = "70e096b6e",
                BusinessCode = "a4dd52a2f81949a5a469ee547e4e991f32d685fd23ff4a33b1caf7a55a5324f8a51f79c7f3f6412099b613ede3",
                BusinessCat = 1950135529,
                TrackerCode = "498f9983918a4e0a8ad7976bcc2a5e4ced3ae1de54a8429db4a9f8b120ed37b151102d51f2a049a09a750a93f2221e",
                DBA = "631e76c",
                FEIN = "b59cb2e86e8b49668ee53ac4cea23c35dc83f9fa95aa4bfeb83fb14631"
            };

            // Act
            var serviceResult = await _businessesAppService.CreateAsync(input);

            // Assert
            var result = await _businessRepository.FindAsync(c => c.Business_ID == serviceResult.Business_ID);

            result.ShouldNotBe(null);
            result.Business_ID.ShouldBe(1480712846);
            result.BusinessName.ShouldBe("70e096b6e");
            result.BusinessCode.ShouldBe("a4dd52a2f81949a5a469ee547e4e991f32d685fd23ff4a33b1caf7a55a5324f8a51f79c7f3f6412099b613ede3");
            result.BusinessCat.ShouldBe(1950135529);
            result.TrackerCode.ShouldBe("498f9983918a4e0a8ad7976bcc2a5e4ced3ae1de54a8429db4a9f8b120ed37b151102d51f2a049a09a750a93f2221e");
            result.DBA.ShouldBe("631e76c");
            result.FEIN.ShouldBe("b59cb2e86e8b49668ee53ac4cea23c35dc83f9fa95aa4bfeb83fb14631");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new BusinessUpdateDto()
            {
                Business_ID = 1403004370,
                BusinessName = "0b865f7ee5e54",
                BusinessCode = "abb534b08b3643a7a2727bc6fb799cf7aac3a4e8a93e",
                BusinessCat = 1767304711,
                TrackerCode = "a65f6df0885246279d4f827738d0f459b10cdd550ab74",
                DBA = "91a0ebf9f73644668",
                FEIN = "21fe825e98b7435c87827a"
            };

            // Act
            var serviceResult = await _businessesAppService.UpdateAsync("1", input);

            // Assert
            var result = await _businessRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Business_ID.ShouldBe(1403004370);
            result.BusinessName.ShouldBe("0b865f7ee5e54");
            result.BusinessCode.ShouldBe("abb534b08b3643a7a2727bc6fb799cf7aac3a4e8a93e");
            result.BusinessCat.ShouldBe(1767304711);
            result.TrackerCode.ShouldBe("a65f6df0885246279d4f827738d0f459b10cdd550ab74");
            result.DBA.ShouldBe("91a0ebf9f73644668");
            result.FEIN.ShouldBe("21fe825e98b7435c87827a");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _businessesAppService.DeleteAsync("1");

            // Assert
            var result = await _businessRepository.FindAsync(c => c.Id == "1");

            result.ShouldBeNull();
        }
    }
}