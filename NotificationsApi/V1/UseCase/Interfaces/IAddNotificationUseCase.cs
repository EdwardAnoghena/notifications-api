using NotificationsApi.V1.Boundary.Request;
using System;
using System.Threading.Tasks;

namespace NotificationsApi.V1.UseCase.Interfaces
{
    public interface IAddNotificationUseCase
    {
        public Task<Guid> ExecuteAsync(NotificationRequest request);
    }
}
