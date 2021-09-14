using NotificationsApi.V1.Boundary.Response;
using NotificationsApi.V1.Factories;
using NotificationsApi.V1.Gateways;
using NotificationsApi.V1.UseCase.Interfaces;
using System.Threading.Tasks;

namespace NotificationsApi.V1.UseCase
{

    public class GetAllNotificationCase : IGetAllNotificationCase
    {
        private readonly INotificationGateway _gateway;
        public GetAllNotificationCase(INotificationGateway gateway)
        {
            _gateway = gateway;
        }

        public async Task<NotificationResponseObjectList> ExecuteAsync()
        {
            var response = await _gateway.GetAllAsync().ConfigureAwait(false);
            return new NotificationResponseObjectList { ResponseObjects = response.ToResponse() };
        }
    }
}
