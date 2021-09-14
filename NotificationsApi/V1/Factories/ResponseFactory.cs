using System.Collections.Generic;
using System.Linq;
using NotificationsApi.V1.Boundary.Response;
using NotificationsApi.V1.Domain;

namespace NotificationsApi.V1.Factories
{
    public static class ResponseFactory
    {
        //TODO: Map the fields in the domain object(s) to fields in the response object(s).
        // More information on this can be found here https://github.com/LBHackney-IT/lbh-base-api/wiki/Factory-object-mappings
        public static NotificationResponseObject ToResponse(this Notification domain)
        {
            return new NotificationResponseObject
            {
                Id = domain.Id,
                TargetId = domain.TargetId,
                TargetType = domain.TargetType,
                AuthorizedBy = domain.AuthorizedBy,
                AuthorizedDate = domain.AuthorizedDate,
                AuthorizerNote = domain.AuthorizerNote,
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
