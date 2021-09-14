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

namespace NotificationsApi.Tests.V1.Gateways
{
    //TODO: Remove this file if DynamoDb gateway not being used
    //TODO: Rename Tests to match gateway name
    //For instruction on how to run tests please see the wiki: https://github.com/LBHackney-IT/lbh-base-api/wiki/Running-the-test-suite.
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
            var response = _classUnderTest.GetEntityById(123);

            response.Should().BeNull();
        }

        [Test]
        public async Task GetEntityByIdReturnsTheEntityIfItExists()
        {
            var entity = _fixture.Create<Notification>();
            var dbEntity = DatabaseEntityHelper.CreateDatabaseEntityFrom(entity);

            _dynamoDb.Setup(x => x.LoadAsync<NotificationEntity>(entity.Id, default))
                     .ReturnsAsync(dbEntity);

            var response = await _classUnderTest.GetEntityByIdAsync(entity.Id).ConfigureAwait(false);

            _dynamoDb.Verify(x => x.LoadAsync<NotificationEntity>(entity.Id, default), Times.Once);

            entity.Id.Should().Be(response.Id);
            entity.CreatedAt.Should().BeSameDateAs(response.CreatedAt);
        }
    }
}
