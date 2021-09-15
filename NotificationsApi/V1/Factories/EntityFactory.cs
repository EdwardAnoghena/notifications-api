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
                //Id = databaseEntity.Id,
                TargetId = databaseEntity.TargetId,
                TargetType = databaseEntity.TargetType,
                Message = databaseEntity.Message,
                ApprovalStatus = databaseEntity.ApprovalStatus,
                AuthorizedBy = databaseEntity.AuthorizedBy,
                AuthorizedDate = databaseEntity.AuthorizedDate,
                AuthorizerNote = databaseEntity.AuthorizerNote,
                IsReadStatus = databaseEntity.IsReadStatus,
                CreatedAt = databaseEntity.CreatedAt
            };
        }

        public static NotificationEntity ToDatabase(this Notification entity)
        {

            return new NotificationEntity
            {
               // Id = entity.Id,
                TargetId = entity.TargetId,
                TargetType = entity.TargetType,
                Message=entity.Message,
                ApprovalStatus = entity.ApprovalStatus,
                AuthorizedBy= entity.AuthorizedBy,
                AuthorizedDate= entity.AuthorizedDate,
                AuthorizerNote= entity.AuthorizerNote,
                IsReadStatus=entity.IsReadStatus,
                CreatedAt = entity.CreatedAt
            };
        }
    }
}
