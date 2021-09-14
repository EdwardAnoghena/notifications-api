using System.Collections.Generic;
using NotificationsApi.V1.Domain;

namespace NotificationsApi.V1.Gateways
{
    public interface IExampleGateway
    {
        Entity GetEntityById(int id);

        List<Entity> GetAll();
    }
}
