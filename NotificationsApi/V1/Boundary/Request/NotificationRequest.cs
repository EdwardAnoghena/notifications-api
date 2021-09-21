using NotificationsApi.V1.Boundary.Response;
using NotificationsApi.V1.Common.Enums;
using System;

namespace NotificationsApi.V1.Boundary.Request
{
    public class NotificationRequest
    {
        public Guid TargetId { get; set; }
        public TargetType TargetType { get; set; }
        public string Message { get; set; }
        public string AdditionalMessage { get; set; }
        public NotificationDetailsObject NotificationObject { get; set; }
    }
}
