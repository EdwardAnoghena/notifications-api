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

        public async Task<ActionResponse> ExecuteAsync(Guid id, AppprovalRequest request)
        {
            await _gateway.UpdateAsync(id, request).ConfigureAwait(false);

            return new ActionResponse { Status = true, Message = "" };
        }
    }
}
