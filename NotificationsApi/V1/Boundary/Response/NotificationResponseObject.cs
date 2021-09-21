using NotificationsApi.V1.Common.Enums;
using System;

namespace NotificationsApi.V1.Boundary.Response
{
    //TODO: Rename to represent to object you will be returning eg. ResidentInformation, HouseholdDetails e.t.c
    public class NotificationResponseObject
    {
        public Guid Id { get; set; }
        public Guid TargetId { get; set; }
        public TargetType TargetType { get; set; }
        public string Message { get; set; }
        public NotificationDetailsObject NotificationDetails { get; set; }
        public bool IsReadStatus { get; set; }
        public bool IsRemovedStatus { get; set; }
        public bool IsApprovalRequired { get; set; }
        public string AuthorizerNote { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public string AuthorizedBy { get; set; }
        public DateTime? AuthorizedDate { get; set; }
        public DateTime CreatedDate { get; set; }
        
    }
}
