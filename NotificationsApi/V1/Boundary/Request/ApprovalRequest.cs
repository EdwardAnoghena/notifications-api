using NotificationsApi.V1.Common.Enums;
using System;

namespace NotificationsApi.V1.Boundary.Request
{
    public class ApprovalRequest
    {
        public ApprovalStatus ApprovalStatus { get; set; }
        public string ApprovalNote { get; set; }
    }
}
