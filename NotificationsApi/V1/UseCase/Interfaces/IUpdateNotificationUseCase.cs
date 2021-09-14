using NotificationsApi.V1.Boundary.Request;
using NotificationsApi.V1.Boundary.Response;
using System;
using System.Threading.Tasks;

namespace NotificationsApi.V1.UseCase.Interfaces
{
    public interface IUpdateNotificationUseCase
    {
        public Task<ActionResponse> ExecuteAsync(Guid id, AppprovalRequest request);
    }
}
