using Amazon.DynamoDBv2.DataModel;
using AutoFixture;
using FluentAssertions;
using Moq;
using NotificationsApi.V1.Boundary.Request;
using NotificationsApi.V1.Boundary.Response;
using NotificationsApi.V1.Common.Enums;
using NotificationsApi.V1.Domain;
using NotificationsApi.V1.Gateways;
using NotificationsApi.V1.UseCase;
using System;
using System.Threading.Tasks;
using Xunit;

namespace NotificationsApi.Tests.V1.UseCase
{
    public class UpdateNotificationUseCaseTest
    {
        private readonly Mock<IDynamoDBContext> _dynamoDb;
        private Mock<INotificationGateway> _mockGateway;
        private UpdateNotificationUseCase _updateUseCase;
        private Fixture _fixture;


        public UpdateNotificationUseCaseTest()
        {
            _dynamoDb = new Mock<IDynamoDBContext>();
            _mockGateway = new Mock<INotificationGateway>();
            _updateUseCase = new UpdateNotificationUseCase(_mockGateway.Object);
            _fixture = new Fixture();
        }
        [Fact]
        public async Task UpdateNotificationUseCaseReturnsTrueResult()
        {
            var targetId = Guid.NewGuid();
            var entity = _fixture.Build<Notification>()
                                .With(_ => _.TargetId, targetId)
                                .With(_ => _.AuthorizedDate, DateTime.UtcNow)
                                .With(_ => _.ApprovalStatus, ApprovalStatus.Approved)
                                .Create();
            var request = _fixture.Build<ApprovalRequest>()
                                 .With(_ => _.ApprovalStatus, ApprovalStatus.Approved)
                                 .Create();
            _mockGateway.Setup(x => x.UpdateAsync(targetId, request)).ReturnsAsync(entity);


            var response = await _updateUseCase.ExecuteAsync(targetId, request)
                .ConfigureAwait(false);
            response.Status.Should().BeTrue();
            response.Message.Should().BeEquivalentTo("Approval successfully");

        }

        [Fact]
        public async Task UpdateNotificationUseCaseReturnsFalseResult()
        {
            var targetId = Guid.NewGuid();

            var request = _fixture.Build<ApprovalRequest>()
                                 .With(_ => _.ApprovalStatus, ApprovalStatus.Approved)
                                 .Create();
            _mockGateway.Setup(x => x.UpdateAsync(targetId, request)).ReturnsAsync((Notification) null);
            var response = await _updateUseCase.ExecuteAsync(targetId, request).ConfigureAwait(false);
            response.Status.Should().BeFalse();
            response.Message.Should().BeEquivalentTo("Approval failed");

        }

        [Fact]
        public void UpdateNotificationAsyncExceptionIsThrown()
        {
            // Arrange
            var request = _fixture.Create<ApprovalRequest>();
            var query = Guid.NewGuid();
            var exception = new ApplicationException("Test exception");
            _mockGateway.Setup(x => x.UpdateAsync(query, request)).ThrowsAsync(exception);

            // Act
            Func<Task<ActionResponse>> func = async () =>
                await _updateUseCase.ExecuteAsync(query, request).ConfigureAwait(false);

            // Assert
            func.Should().Throw<ApplicationException>().WithMessage(exception.Message);
        }
    }
}
