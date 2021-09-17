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
            await _gateway.UpdateAsync(id, request).ConfigureAwait(false);
            var rId = await _gateway.GetEntityByIdAsync(id).ConfigureAwait(false);
            var status = rId.AuthorizedDate.HasValue && (rId.AuthorizedDate.Value.Date == DateTime.Today.Date);

            return new ActionResponse { Status = status, Message = status ? "Approval successfully" : "Approval Failed" };
        }
    }
}
