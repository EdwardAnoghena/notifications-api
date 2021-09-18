using AutoFixture;
using NotificationsApi.V1.Domain;
using NotificationsApi.V1.Factories;
using NotificationsApi.V1.Infrastructure;
using FluentAssertions;
using Xunit;

namespace NotificationsApi.Tests.V1.Factories
{

    public class EntityFactoryTest
    {
        private readonly Fixture _fixture = new Fixture();

        [Fact]
        public void CanMapADatabaseEntityToADomainObject()
        {
            var databaseEntity = _fixture.Create<NotificationEntity>();
            var entity = databaseEntity.ToDomain();

            databaseEntity.TargetId.Should().Be(entity.TargetId);
            databaseEntity.CreatedAt.Should().BeSameDateAs(entity.CreatedAt);
            databaseEntity.ApprovalStatus.Should().Be(entity.ApprovalStatus);
            databaseEntity.AuthorizedDate.Should().BeSameDateAs(entity.AuthorizedDate.Value);
            databaseEntity.AuthorizerNote.Should().BeEquivalentTo(entity.AuthorizerNote);
            databaseEntity.AuthorizedBy.Should().BeEquivalentTo(entity.AuthorizedBy);
            databaseEntity.IsReadStatus.Should().Be(entity.IsReadStatus);
            databaseEntity.TargetType.Should().Be(entity.TargetType);
            databaseEntity.Message.Should().BeEquivalentTo(entity.Message);
        }

        [Fact]
        public void CanMapADomainEntityToADatabaseObject()
        {
            var entity = _fixture.Create<Notification>();
            var databaseEntity = entity.ToDatabase();

            entity.TargetId.Should().Be(databaseEntity.TargetId);
            entity.CreatedAt.Should().BeSameDateAs(databaseEntity.CreatedAt);
            entity.ApprovalStatus.Should().Be(databaseEntity.ApprovalStatus);
            entity.AuthorizedDate.Should().BeSameDateAs(databaseEntity.AuthorizedDate.Value);
            entity.AuthorizerNote.Should().BeEquivalentTo(databaseEntity.AuthorizerNote);
            entity.AuthorizedBy.Should().BeEquivalentTo(databaseEntity.AuthorizedBy);
            entity.IsReadStatus.Should().Be(databaseEntity.IsReadStatus);
            entity.TargetType.Should().Be(databaseEntity.TargetType);
            entity.Message.Should().BeEquivalentTo(databaseEntity.Message);
        }
    }
}
