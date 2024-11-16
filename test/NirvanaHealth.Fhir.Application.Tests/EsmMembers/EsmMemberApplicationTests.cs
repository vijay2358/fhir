//using System;
//using System.Linq;
//using Shouldly;
//using System.Threading.Tasks;
//using Volo.Abp.Domain.Repositories;
//using Xunit;

//namespace NirvanaHealth.Fhir.EsmMembers
//{
//    public class EsmMembersAppServiceTests : FhirApplicationTestBase
//    {
//        private readonly IEsmMembersAppService _esmMembersAppService;
//        private readonly IRepository<EsmMember, string> _esmMemberRepository;

//        public EsmMembersAppServiceTests()
//        {
//            _esmMembersAppService = GetRequiredService<IEsmMembersAppService>();
//            _esmMemberRepository = GetRequiredService<IRepository<EsmMember, string>>();
//        }

//        [Fact]
//        public async Task GetListAsync()
//        {
//            // Act
//            var result = await _esmMembersAppService.GetListAsync(new GetEsmMembersInput());

//            // Assert
//            result.TotalCount.ShouldBe(2);
//            result.Items.Count.ShouldBe(2);
//            result.Items.Any(x => x.Id == Guid.Parse("707dd627-c4a5-46ee-be75-d584f8036ee9")).ShouldBe(true);
//            result.Items.Any(x => x.Id == Guid.Parse("90e80673-568b-46de-aaa2-e872493e13d3")).ShouldBe(true);
//        }

//        [Fact]
//        public async Task GetAsync()
//        {
//            // Act
//            var result = await _esmMembersAppService.GetAsync(("707dd627-c4a5-46ee-be75-d584f8036ee9"));

//            // Assert
//            result.ShouldNotBeNull();
//            result.Id.ShouldBe(Guid.Parse("707dd627-c4a5-46ee-be75-d584f8036ee9"));
//        }

//        [Fact]
//        public async Task CreateAsync()
//        {
//            // Arrange
//            var input = new EsmMemberCreateDto
//            {
//                MCPMember_ID = 581673934,
//                LastName = "df1c2454f8ba4",
//                FirstName = "6fef5ddc69694f849a939fabf67525ed1",
//                MIddleName = "a857dc7d57b74a859e6c153861e0a009cd",
//                Suffix = "c93ab006a5134cf08c4312db9efe00226bb3389d38ce4ea3b74cea",
//                PreFix = "307b320f3098428cb8d8bb165df4760dbf7b37c550e242229adcf5b51d5a1734",
//                DOB = new DateTime(2012, 4, 15),
//                Gender = default,
//                DateofDeath = new DateTime(2002, 6, 9),
//                Race = default
//            };

//            // Act
//            var serviceResult = await _esmMembersAppService.CreateAsync(input);

//            // Assert
//            var result = await _esmMemberRepository.FindAsync(c => c.Id == serviceResult.Id);

//            result.ShouldNotBe(null);
//            result.MCPMember_ID.ShouldBe(581673934);
//            result.LastName.ShouldBe("df1c2454f8ba4");
//            result.FirstName.ShouldBe("6fef5ddc69694f849a939fabf67525ed1");
//            result.MIddleName.ShouldBe("a857dc7d57b74a859e6c153861e0a009cd");
//            result.Suffix.ShouldBe("c93ab006a5134cf08c4312db9efe00226bb3389d38ce4ea3b74cea");
//            result.PreFix.ShouldBe("307b320f3098428cb8d8bb165df4760dbf7b37c550e242229adcf5b51d5a1734");
//            result.DOB.ShouldBe(new DateTime(2012, 4, 15));
//            result.Gender.ShouldBe(default);
//            result.DateofDeath.ShouldBe(new DateTime(2002, 6, 9));
//            result.Race.ShouldBe(default);
//        }

//        [Fact]
//        public async Task UpdateAsync()
//        {
//            // Arrange
//            var input = new EsmMemberUpdateDto()
//            {
//                MCPMember_ID = 1988542449,
//                LastName = "176bdde6ab8f433288611965efbf173e3f66e7cdb0c8464c9b6d32dc67462b60c485e07bda50",
//                FirstName = "153895b3fa1d4c9590fab164e2583bdbc72884d9a1094fc48cdde37c14798c8d1a67eef9529",
//                MIddleName = "9ef99122d4bc4d85a8159faf1c51ab5342828c214e9a470988",
//                Suffix = "356e8464658e454fad1aa68f76e82c4d8b5af4a272ed471db213870f8fc4a082354fd07",
//                PreFix = "be0458fbd6b64cc29e2b132f5690",
//                DOB = new DateTime(2019, 8, 13),
//                Gender = default,
//                DateofDeath = new DateTime(2014, 2, 27),
//                Race = default
//            };

//            // Act
//            var serviceResult = await _esmMembersAppService.UpdateAsync(("707dd627-c4a5-46ee-be75-d584f8036ee9"), input);

//            // Assert
//            var result = await _esmMemberRepository.FindAsync(c => c.Id == serviceResult.Id);

//            result.ShouldNotBe(null);
//            result.MCPMember_ID.ShouldBe(1988542449);
//            result.LastName.ShouldBe("176bdde6ab8f433288611965efbf173e3f66e7cdb0c8464c9b6d32dc67462b60c485e07bda50");
//            result.FirstName.ShouldBe("153895b3fa1d4c9590fab164e2583bdbc72884d9a1094fc48cdde37c14798c8d1a67eef9529");
//            result.MIddleName.ShouldBe("9ef99122d4bc4d85a8159faf1c51ab5342828c214e9a470988");
//            result.Suffix.ShouldBe("356e8464658e454fad1aa68f76e82c4d8b5af4a272ed471db213870f8fc4a082354fd07");
//            result.PreFix.ShouldBe("be0458fbd6b64cc29e2b132f5690");
//            result.DOB.ShouldBe(new DateTime(2019, 8, 13));
//            result.Gender.ShouldBe(default);
//            result.DateofDeath.ShouldBe(new DateTime(2014, 2, 27));
//            result.Race.ShouldBe(default);
//        }

//        [Fact]
//        public async Task DeleteAsync()
//        {
//            // Act
//            await _esmMembersAppService.DeleteAsync("707dd627-c4a5-46ee-be75-d584f8036ee9");

//            // Assert
//            var result = await _esmMemberRepository.FindAsync(c => c.Id == ("707dd627-c4a5-46ee-be75-d584f8036ee9"));

//            result.ShouldBeNull();
//        }
//    }
//}