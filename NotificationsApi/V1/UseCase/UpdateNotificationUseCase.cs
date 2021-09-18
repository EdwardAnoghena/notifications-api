using NotificationsApi.V1.Boundary.Request;
using NotificationsApi.V1.Boundary.Response;
using NotificationsApi.V1.Gateways;
using NotificationsApi.V1.UseCase.Interfaces;
using System;
using System.Threading.Tasks;

namespace NotificationsApi.V1.UseCase
{
    public class UpdateNotificationUseCase : IUpdateNotificationUseCase
    {
        private readonly INotificationGateway _gateway;

        public UpdateNotificationUseCase(INotificationGateway gateway)
        {
            _gateway = gateway;
        }

        public async Task<ActionResponse> ExecuteAsync(Guid id, ApprovalRequest request)
        {
            var notification = await _gateway.UpdateAsync(id, request).ConfigureAwait(false);

            var status = notification != null && notification.AuthorizedDate.HasValue && (notification.AuthorizedDate.Value.Date == DateTime.Today.Date);

            return new ActionResponse { Status = status, Message = status ? "Approval successfully" : "Approval failed" };
        }
    }
}
