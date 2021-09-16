using AutoFixture;
using NotificationsApi.V1.Domain;
using NotificationsApi.V1.Infrastructure;

namespace NotificationsApi.Tests.V1.Helper
{
    public static class DatabaseEntityHelper
    {
        public static NotificationEntity CreateDatabaseEntity()
        {
            var entity = new Fixture().Create<Notification>();

            return CreateDatabaseEntityFrom(entity);
        }

        public static NotificationEntity CreateDatabaseEntityFrom(Notification entity)
        {
            return new NotificationEntity
            {
                //Id = entity.Id,
                TargetId = entity.TargetId,
                TargetType = entity.TargetType,
                ApprovalStatus = entity.ApprovalStatus,
                AuthorizedBy = entity.AuthorizedBy,
                AuthorizedDate = entity.AuthorizedDate,
                AuthorizerNote = entity.AuthorizerNote,
                IsReadStatus = entity.IsReadStatus,
                Message = entity.Message,
                CreatedAt = entity.CreatedAt,
            };
        }
    }
}
