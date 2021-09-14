using NotificationsApi.V1.Boundary.Response;
using System;
using System.Threading.Tasks;

namespace NotificationsApi.V1.UseCase.Interfaces
{
    public interface IGetTargetDetailsCase
    {
        Task<NotificationDetailsObject> ExecuteAsync(Guid targetId);
    }
}
