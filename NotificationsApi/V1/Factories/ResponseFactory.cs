using System.Collections.Generic;
using System.Linq;
using NotificationsApi.V1.Boundary.Response;
using NotificationsApi.V1.Domain;

namespace NotificationsApi.V1.Factories
{
    public static class ResponseFactory
    {
        
        public static NotificationResponseObject ToResponse(this Notification domain)
        {
            return new NotificationResponseObject
            {
                //Id = domain.Id,
                TargetId = domain.TargetId,
                TargetType = domain.TargetType,
                AuthorizedBy = domain.AuthorizedBy,
                AuthorizedDate = domain.AuthorizedDate,
                AuthorizerNote = domain.AuthorizerNote,
                ApprovalStatus= domain.ApprovalStatus,
                IsReadStatus = domain.IsReadStatus,
                Message = domain.Message,
                CreatedDate = domain.CreatedAt
            };
        }

        public static List<NotificationResponseObject> ToResponse(this IEnumerable<Notification> domainList)
        {
            return domainList.Select(domain => domain.ToResponse()).ToList();
        }
    }
}
