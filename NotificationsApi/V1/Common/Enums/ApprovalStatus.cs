using System.Text.Json.Serialization;

namespace NotificationsApi.V1.Common.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ApprovalStatus
    {
        Initiate, Approve, Reject
    }
}
