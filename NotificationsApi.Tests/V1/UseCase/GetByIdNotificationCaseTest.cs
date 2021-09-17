using NotificationsApi.V1.Gateways;
using NotificationsApi.V1.UseCase;
using Moq;
using Xunit;
using System.Threading.Tasks;
using AutoFixture;
using NotificationsApi.V1.Domain;
using NotificationsApi.V1.Factories;
using FluentAssertions;
using System;
using NotificationsApi.V1.Boundary.Response;

namespace NotificationsApi.Tests.V1.UseCase
{
    public class GetByIdNotificationCaseTest
    {
        private Mock<INotificationGateway> _mockGateway;
        private GetByIdNotificationCase _classUnderTest;
        private Fixture _fixture;


        public GetByIdNotificationCaseTest()
        {
            _mockGateway = new Mock<INotificationGateway>();
            _classUnderTest = new GetByIdNotificationCase(_mockGateway.Object);
            _fixture = new Fixture();
        }

        [Fact]
        public async Task GetByIdUsecaseShouldReturnOkResponse()
        {
            var stubbedEntity = _fixture.Create<Notification>();
            _mockGateway.Setup(x => x.GetEntityByIdAsync(stubbedEntity.TargetId)).ReturnsAsync(stubbedEntity);
            var expectedResponse = stubbedEntity.ToResponse();

            var response = await _classUnderTest.ExecuteAsync(stubbedEntity.TargetId).ConfigureAwait(false);
            response.Should().NotBeNull();
            response.Should().BeEquivalentTo(expectedResponse);
           
        }

        [Fact]
        public async Task GetByIdUsecaseShouldBeNull()
        {
          
            var targetId = Guid.NewGuid();
            _mockGateway.Setup(x => x.GetEntityByIdAsync(targetId)).ReturnsAsync((Notification)null);


            var response = await _classUnderTest.ExecuteAsync(targetId).ConfigureAwait(false);
            response.Should().BeNull();

        }

        [Fact]
        public void GetByIdThrowsException()
        {
            var request = Guid.NewGuid();
            var exception = new ApplicationException("Test Exception");
            _mockGateway.Setup(x => x.GetEntityByIdAsync(request)).ThrowsAsync(exception);
            Func<Task<NotificationResponseObject>> throwException = async () => await _classUnderTest.ExecuteAsync(request).ConfigureAwait(false);
            throwException.Should().Throw<ApplicationException>().WithMessage("Test Exception");
        }
    }
}
