using NotificationsApi.V1.Common.Enums;

namespace NotificationsApi.V1.Boundary.Request
{
    public class AppprovalRequest
    {
        public ApprovalStatus ApprovalStatus { get; set; }
        public string ApprovalNote { get; set; }
    }
}
