using NotificationsApi.V1.Boundary.Response;
using System.Threading.Tasks;

namespace NotificationsApi.V1.UseCase.Interfaces
{
    public interface IGetAllNotificationCase
    {
        Task<NotificationResponseObjectList> ExecuteAsync();
    }
}
