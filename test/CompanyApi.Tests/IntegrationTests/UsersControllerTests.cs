using System.Collections.Generic;
using System.Threading.Tasks;
using CompanyApi.Contracts.Entities;
using CompanyApi.Tests.Services;
using Xunit;

namespace CompanyApi.Tests.IntegrationTests
{
    public class UsersControllerTests : IClassFixture<TestWebApplicationFactory>
    {
        private readonly HttpClientHelper _httpClientHelper;

        public UsersControllerTests(TestWebApplicationFactory factory)
        {
            _httpClientHelper = new HttpClientHelper(factory.CreateClient());
        }

        [Fact]
        public async Task CanGetAllUsers()
        {
            var users = await _httpClientHelper.GetAsync<List<User>>("/users/getall");
            Assert.Contains(users, p => p.Username == "johnw");
        }

        [Fact]
        public async Task CanUserAuthenticate()
        {
            var user = new User { EmployeeId = 1, Username = "johnw", Password = "test", Token = string.Empty };
            var authenticatedUser = await _httpClientHelper.PostAsync("/users/authenticate", user);
            Assert.NotNull(authenticatedUser);
        }
    }
}
