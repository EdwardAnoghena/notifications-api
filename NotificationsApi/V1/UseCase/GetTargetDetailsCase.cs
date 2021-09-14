using NotificationsApi.V1.Boundary.Response;
using NotificationsApi.V1.UseCase.Interfaces;
using System;
using System.Threading.Tasks;

namespace NotificationsApi.V1.UseCase
{
    public class GetTargetDetailsCase : IGetTargetDetailsCase
    {
        public Task<NotificationDetailsObject> ExecuteAsync(Guid targetId)
        {
            throw new NotImplementedException();
        }
    }
}
