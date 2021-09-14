using System.Text.Json.Serialization;

namespace NotificationsApi.V1.Common.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TargetType
    {
        FailedDirectDebits,
        MissedServiceChargePayments,
        Estimates,
        Actuals,
        ViewNewProperty,
        ValuateNewProperty,
        ViewNewTenancy,
        ApproveSuspenseAccountTransfer
    }
}
