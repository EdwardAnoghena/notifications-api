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

        //TODO: add assertions for all the fields being mapped in `EntityFactory.ToDomain()`. Also be sure to add test cases for
        // any edge cases that might exist.
        [Fact]
        public void CanMapADatabaseEntityToADomainObject()
        {
            var databaseEntity = _fixture.Create<NotificationEntity>();
            var entity = databaseEntity.ToDomain();

            databaseEntity.TargetId.Should().Be(entity.TargetId);
            databaseEntity.CreatedAt.Should().BeSameDateAs(entity.CreatedAt);
        }

        //TODO: add assertions for all the fields being mapped in `EntityFactory.ToDatabase()`. Also be sure to add test cases for
        // any edge cases that might exist.
        [Fact]
        public void CanMapADomainEntityToADatabaseObject()
        {
            var entity = _fixture.Create<Notification>();
            var databaseEntity = entity.ToDatabase();

            entity.TargetId.Should().Be(databaseEntity.TargetId);
            entity.CreatedAt.Should().BeSameDateAs(databaseEntity.CreatedAt);
        }
    }
}
