using System;
using NotificationsApi.V1.Domain;
using FluentAssertions;
using Xunit;

namespace NotificationsApi.Tests.V1.Domain
{

    public class EntityTests
    {
        [Fact]
        public void EntitiesHaveAnId()
        {
            var entity = new Notification { TargetId = Guid.NewGuid() };
            entity.TargetId.Should().NotBeEmpty();//.BeGreaterOrEqualTo(0);
        }

        [Fact]
        public void EntitiesHaveACreatedAt()
        {
            var entity = new Notification();
            var date = new DateTime(2019, 02, 21);
            entity.CreatedAt = date;

            entity.CreatedAt.Should().BeSameDateAs(date);
        }
    }
}
