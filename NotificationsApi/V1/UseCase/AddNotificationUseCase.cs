using NotificationsApi.V1.Boundary.Request;
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
            var notification = new Notification { TargetId = request.TargetId, TargetType = request.TargetType };
            await _gateway.AddAsync(notification).ConfigureAwait(false);
            return notification.Id;
        }
    }
}
