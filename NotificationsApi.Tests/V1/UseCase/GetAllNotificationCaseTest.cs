using AutoFixture;
using FluentAssertions;
using Moq;
using NotificationsApi.V1.Boundary.Response;
using NotificationsApi.V1.Domain;
using NotificationsApi.V1.Factories;
using NotificationsApi.V1.Gateways;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace NotificationsApi.Tests.V1.UseCase
{
    public class GetAllNotificationCaseTest
    {
        private Mock<INotificationGateway> _mockGateway;
        private NotificationsApi.V1.UseCase.GetAllNotificationCase _classUnderTest;
        private Fixture _fixture;

        public GetAllNotificationCaseTest()
        {
            _mockGateway = new Mock<INotificationGateway>();
            _classUnderTest = new NotificationsApi.V1.UseCase.GetAllNotificationCase(_mockGateway.Object);
            _fixture = new Fixture();
        }

        [Fact]
        public async Task GetsAllFromTheGateway()
        {
            var stubbedEntities = _fixture.CreateMany<Notification>().ToList();
            _mockGateway.Setup(x => x.GetAllAsync()).ReturnsAsync(stubbedEntities);

            var expectedResponse = new NotificationResponseObjectList { ResponseObjects = stubbedEntities.ToResponse() };

            var response = await _classUnderTest.ExecuteAsync().ConfigureAwait(false);
            response.Should().BeEquivalentTo(expectedResponse);
        }

        [Fact]
        public async Task GetSingleOneFromTheGateway()
        {
            var stubbedEntities = _fixture.CreateMany<Notification>(1).ToList();
            _mockGateway.Setup(x => x.GetAllAsync()).ReturnsAsync(stubbedEntities);
            var expectedResponse = new NotificationResponseObjectList { ResponseObjects = stubbedEntities.ToResponse() };

            var response = await _classUnderTest.ExecuteAsync().ConfigureAwait(false);
            response.Should().BeEquivalentTo(expectedResponse);
            response.ResponseObjects.Should().HaveCount(1);
        }

    }
}
