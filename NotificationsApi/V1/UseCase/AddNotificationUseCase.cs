using NotificationsApi.V1.Boundary.Request;
using NotificationsApi.V1.Common.Enums;
using NotificationsApi.V1.Domain;
using NotificationsApi.V1.Gateways;
using NotificationsApi.V1.UseCase.Interfaces;
using System;
using System.Threading.Tasks;

namespace NotificationsApi.V1.UseCase
{
    public class AddNotificationUseCase : IAddNotificationUseCase
    {
        private readonly INotificationGateway _gateway;

        public AddNotificationUseCase(INotificationGateway gateway)
        {
            _gateway = gateway;
        }

        public async Task<Guid> ExecuteAsync(NotificationRequest request)
        {
            var notification = new Notification { TargetId = request.TargetId, TargetType = request.TargetType, Message = GetMessage(request.TargetType) };
            await _gateway.AddAsync(notification).ConfigureAwait(false);
            return notification.TargetId;
        }

        private static string GetMessage(TargetType targetType)
        {
            var message = string.Empty;
            switch (targetType)
            {
                case TargetType.FailedDirectDebits:
                    message = "Direct Debit failed";
                    break;
                case TargetType.MissedServiceChargePayments:
                    message = "3 missed service charge payments have exceeded the tolerance period";
                    break;
                case TargetType.Estimates:
                    message = "2021-2022 estimates have been sent for your approval";
                    break;
                case TargetType.Actuals:
                    message = "2019-2020 Actuals have been approved";
                    break;
                case TargetType.ViewNewProperty:
                    message = "A new property has been added  12/12/12";
                    break;
                case TargetType.ValuateNewProperty:
                    message = "A new property has been added  12/12/12";
                    break;
                case TargetType.ViewNewTenancy:
                    message = "2 Adjustments approved  12/12/12";
                    break;
                case TargetType.ApproveSuspenseAccountTransfer:
                    message = "A Journal Transfer is awaiting your approval  12/12/12";
                    break;
                default:
                    break;
            }
            return message;
        }
    }
}
