using NotificationsApi.V1.Boundary.Request;
using NotificationsApi.V1.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NotificationsApi.V1.Gateways
{
    public interface INotificationGateway
    {
        Task<Notification> GetEntityByIdAsync(Guid id);

        Task<List<Notification>> GetAllAsync();
        Task AddAsync(Notification notification);

        Task UpdateAsync(Guid id, AppprovalRequest notification);
    }
}
