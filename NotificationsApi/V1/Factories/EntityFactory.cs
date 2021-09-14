using NotificationsApi.V1.Domain;
using NotificationsApi.V1.Infrastructure;

namespace NotificationsApi.V1.Factories
{
    public static class EntityFactory
    {
        public static Notification ToDomain(this NotificationEntity databaseEntity)
        {

            return new Notification
            {
                Id = databaseEntity.Id,
                TargetId = databaseEntity.TargetId,
                TargetType = databaseEntity.TargetType,
                CreatedAt = databaseEntity.CreatedAt
            };
        }

        public static NotificationEntity ToDatabase(this Notification entity)
        {

            return new NotificationEntity
            {
                Id = entity.Id,
                TargetId = entity.TargetId,
                TargetType = entity.TargetType,
                CreatedAt = entity.CreatedAt
            };
        }
    }
}
