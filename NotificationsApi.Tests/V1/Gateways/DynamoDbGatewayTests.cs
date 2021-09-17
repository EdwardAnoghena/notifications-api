using Amazon.DynamoDBv2.DataModel;
using AutoFixture;
using NotificationsApi.Tests.V1.Helper;
using NotificationsApi.V1.Domain;
using NotificationsApi.V1.Gateways;
using NotificationsApi.V1.Infrastructure;
using FluentAssertions;
using Moq;
using System.Threading.Tasks;
using System;
using Xunit;
using NotificationsApi.V1.Factories;
using System.Collections.Generic;
using System.Threading;
using NotificationsApi.V1.Boundary.Request;
using NotificationsApi.V1.Common.Enums;
using System.Linq;

namespace NotificationsApi.Tests.V1.Gateways
{

    public class DynamoDbGatewayTests : IDisposable
    {
        private readonly Fixture _fixture = new Fixture();
        private readonly Mock<IDynamoDBContext> _dynamoDb;
        private readonly DynamoDbGateway _classUnderTest;
        private readonly List<Action> _cleanup = new List<Action>();

        public DynamoDbGatewayTests()
        {
            _dynamoDb = new Mock<IDynamoDBContext>();
            _classUnderTest = new DynamoDbGateway(_dynamoDb.Object);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool _disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !_disposed)
            {
                foreach (var action in _cleanup)
                    action();

                _disposed = true;
            }
        }
        [Fact]
        public void GetAllNotifications()
        {
            //var entities = _fixture.Build<Notification>()
            //    .With(x => x.TargetId, Guid.NewGuid()).CreateMany(3).ToList();

            //var entity = entities.FirstOrDefault();

            //var databaseEntities = entities.Select(entity => entity.ToDatabase());

            //_dynamoDb.Setup(x => x.ScanAsync(
            //    It.IsAny<IEnumerable<ScanCondition>>(),
            //    It.IsAny<DynamoDBOperationConfig>()))
            //    .Returns((databaseEntities));

            //var response = await _gateway.GetAllTransactionsAsync(entity.TargetId, entity.TransactionType, entity.TransactionDate, entity.TransactionDate).ConfigureAwait(false);

            //response.Should().BeEquivalentTo(entities);
        }
        [Fact]
        public void GetEntityByIdReturnsNullIfEntityDoesntExist()
        {
            var guid = Guid.NewGuid();
            var response = _classUnderTest.GetEntityById(guid);

            response.Should().BeNull();
        }

        [Fact]
        public async Task GetEntityByIdReturnsTheEntityIfItExists()
        {
            var entity = _fixture.Create<Notification>();
            var dbEntity = DatabaseEntityHelper.CreateDatabaseEntityFrom(entity);

            _dynamoDb.Setup(x => x.LoadAsync<NotificationEntity>(entity.TargetId, default))
                     .ReturnsAsync(dbEntity);

            var response = await _classUnderTest.GetEntityByIdAsync(entity.TargetId).ConfigureAwait(false);

            _dynamoDb.Verify(x => x.LoadAsync<NotificationEntity>(entity.TargetId, default), Times.Once);

            entity.TargetId.Should().Be(response.TargetId);
            entity.CreatedAt.Should().BeSameDateAs(response.CreatedAt);
        }


        [Fact]
        public async Task PostNewNotificationSuccessfulSaves()
        {
            // Arrange
            var entityRequest = _fixture.Build<Notification>()
                                 .Create();
            _dynamoDb.Setup(x => x.SaveAsync(It.IsAny<NotificationEntity>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
            // Act
            await _classUnderTest.AddAsync(entityRequest).ConfigureAwait(false);

            // Assert
            _dynamoDb.Verify(x => x.SaveAsync(It.IsAny<NotificationEntity>(), default), Times.Once);

        }


        [Fact]
        public async Task UpdateExistingNotificationSuccessfulSaves()
        {
            // Arrange
            var entityRequest = _fixture.Build<Notification>()
                                   .With(x => x.ApprovalStatus, ApprovalStatus.Initiate)
                                 .Create();
            var dbEntity = DatabaseEntityHelper.CreateDatabaseEntityFrom(entityRequest);
            _dynamoDb.Setup(x => x.LoadAsync<NotificationEntity>(entityRequest.TargetId, default))
                     .ReturnsAsync(dbEntity);

            var approvalRequest = new ApprovalRequest { ApprovalNote = "Approval Note", ApprovalStatus = ApprovalStatus.Approve };

            _dynamoDb.Setup(x => x.SaveAsync(It.IsAny<NotificationEntity>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
            // Act
            await _classUnderTest.UpdateAsync(entityRequest.TargetId, approvalRequest).ConfigureAwait(false);

            // Assert

            _dynamoDb.Verify(x => x.SaveAsync(It.IsAny<NotificationEntity>(), default), Times.Once);
            var load = await _classUnderTest.GetEntityByIdAsync(entityRequest.TargetId).ConfigureAwait(false);
            load.AuthorizerNote.Should().BeEquivalentTo(approvalRequest.ApprovalNote);
            load.ApprovalStatus.Should().Be(approvalRequest.ApprovalStatus);
            load.AuthorizedDate.Value.Date.Should().BeSameDateAs(DateTime.UtcNow.Date);

        }

        [Fact]
        public async Task UpdateExistingNotificationFailedSaves()
        {
            // Arrange
            var guid = Guid.NewGuid();

            var approvalRequest = new ApprovalRequest { ApprovalNote = "Approval Note", ApprovalStatus = ApprovalStatus.Approve };

            _dynamoDb.Setup(x => x.SaveAsync(It.IsAny<NotificationEntity>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
            // Act
            await _classUnderTest.UpdateAsync(guid, approvalRequest).ConfigureAwait(false);

            // Assert

            _dynamoDb.Verify(x => x.SaveAsync(It.IsAny<NotificationEntity>(), default), Times.Never);
            var load = await _classUnderTest.GetEntityByIdAsync(guid).ConfigureAwait(false);
            load.Should().BeNull();

        }


    }
}
