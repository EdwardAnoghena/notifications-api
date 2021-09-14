using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NotificationsApi.V1.Domain;
using NotificationsApi.V1.Factories;
using NotificationsApi.V1.Infrastructure;

namespace NotificationsApi.V1.Gateways
{
    //TODO: Rename to match the data source that is being accessed in the gateway eg. MosaicGateway
    public class ExampleGateway : INotificationGateway
    {
        private readonly DatabaseContext _databaseContext;

        public ExampleGateway(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public Notification GetEntityById(int id)
        {
            var result = _databaseContext.DatabaseEntities.Find(id);

            return result?.ToDomain();
        }

        //public List<Notification> GetAll()
        //{
        //    return new List<Notification>();
        //}

        public Task<Notification> GetEntityByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Notification>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(Notification notification)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Guid id, Notification notification)
        {
            throw new NotImplementedException();
        }
    }
}
