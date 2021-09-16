using Amazon.DynamoDBv2.DataModel;
using AutoFixture;
using NotificationsApi.Tests.V1.Helper;
using NotificationsApi.V1.Domain;
using NotificationsApi.V1.Gateways;
using NotificationsApi.V1.Infrastructure;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using System;

namespace NotificationsApi.Tests.V1.Gateways
{

    [TestFixture]
    public class DynamoDbGatewayTests
    {
        private readonly Fixture _fixture = new Fixture();
        private Mock<IDynamoDBContext> _dynamoDb;
        private DynamoDbGateway _classUnderTest;

        [SetUp]
        public void Setup()
        {
            _dynamoDb = new Mock<IDynamoDBContext>();
            _classUnderTest = new DynamoDbGateway(_dynamoDb.Object);
        }

        [Test]
        public void GetEntityByIdReturnsNullIfEntityDoesntExist()
        {
            var guid = Guid.NewGuid();
            var response = _classUnderTest.GetEntityById(guid);

            response.Should().BeNull();
        }

        [Test]
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
    }
}
