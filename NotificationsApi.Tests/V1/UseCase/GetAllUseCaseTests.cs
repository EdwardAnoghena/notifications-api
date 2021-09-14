using System.Linq;
using AutoFixture;
using NotificationsApi.V1.Boundary.Response;
using NotificationsApi.V1.Domain;
using NotificationsApi.V1.Factories;
using NotificationsApi.V1.Gateways;
using NotificationsApi.V1.UseCase;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace NotificationsApi.Tests.V1.UseCase
{
    public class GetAllUseCaseTests
    {
        private Mock<INotificationGateway> _mockGateway;
        private GetAllNotificationCase _classUnderTest;
        private Fixture _fixture;

        [SetUp]
        public void SetUp()
        {
            _mockGateway = new Mock<INotificationGateway>();
            _classUnderTest = new GetAllNotificationCase(_mockGateway.Object);
            _fixture = new Fixture();
        }

        [Test]
        public async Task GetsAllFromTheGateway()
        {
            var stubbedEntities = _fixture.CreateMany<Notification>().ToList();
            _mockGateway.Setup(x => x.GetAllAsync()).ReturnsAsync(stubbedEntities);

            var expectedResponse = new NotificationResponseObjectList { ResponseObjects = stubbedEntities.ToResponse() };

            var response = await _classUnderTest.ExecuteAsync().ConfigureAwait(false);
            response.Should().BeEquivalentTo(expectedResponse);
        }

        //TODO: Add extra tests here for extra functionality added to the use case
    }
}
