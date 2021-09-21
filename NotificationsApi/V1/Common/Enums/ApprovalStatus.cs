using System.Text.Json.Serialization;

namespace NotificationsApi.V1.Common.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ApprovalStatus
    {
        Initiated, Approved, Rejected
    }
}
