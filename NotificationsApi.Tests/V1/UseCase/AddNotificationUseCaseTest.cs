using AutoFixture;
using FluentAssertions;
using Moq;
using NotificationsApi.V1.Boundary.Request;
using NotificationsApi.V1.Domain;
using NotificationsApi.V1.Gateways;
using NotificationsApi.V1.UseCase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace NotificationsApi.Tests.V1.UseCase
{
    public class AddNotificationUseCaseTest
    {
        private Mock<INotificationGateway> _mockGateway;
        private AddNotificationUseCase _addUseCase;
        private Fixture _fixture;


        public AddNotificationUseCaseTest()
        {
            _mockGateway = new Mock<INotificationGateway>();
            _addUseCase = new AddNotificationUseCase(_mockGateway.Object);
            _fixture = new Fixture();
        }
        [Fact]
        public async Task AddNotificationReturnSuccessSave()
        {
            var entity = _fixture.Create<NotificationRequest>();
            _mockGateway.Setup(x => x.AddAsync(It.IsAny<Notification>()))
                .Returns(Task.CompletedTask);
            var response = await _addUseCase.ExecuteAsync(entity)
                .ConfigureAwait(false);
            response.Should().Be(entity.TargetId);

            _mockGateway.Verify(x => x.AddAsync(It.IsAny<Notification>()), Times.Once);
        }
    }
}
