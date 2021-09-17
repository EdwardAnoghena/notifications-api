using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using NotificationsApi.V1.Boundary.Request;
using NotificationsApi.V1.Boundary.Response;
using NotificationsApi.V1.Domain;
using NotificationsApi.V1.Factories;
using NotificationsApi.V1.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationsApi.V1.Gateways
{
    public class DynamoDbGateway : INotificationGateway
    {
        private readonly IDynamoDBContext _dynamoDbContext;

        public DynamoDbGateway(IDynamoDBContext dynamoDbContext)
        {
            _dynamoDbContext = dynamoDbContext;
        }

        public async Task AddAsync(Notification notification)
        {
            var dbEntity = notification.ToDatabase();
            await _dynamoDbContext.SaveAsync(dbEntity).ConfigureAwait(false);

        }

        public async Task<List<Notification>> GetAllAsync()
        {
            List<ScanCondition> conditions = new List<ScanCondition>();
            DynamoDBOperationConfig operationConfig = null;
            var data = await _dynamoDbContext.ScanAsync<NotificationEntity>(conditions, operationConfig).GetRemainingAsync().ConfigureAwait(false);
            return data.Select(x => x.ToDomain()).ToList();
        }

        public Notification GetEntityById(Guid id)
        {
            var result = _dynamoDbContext.LoadAsync<NotificationEntity>(id).GetAwaiter().GetResult();
            return result?.ToDomain();
        }

        public async Task<Notification> GetEntityByIdAsync(Guid id)
        {
            var result = await _dynamoDbContext.LoadAsync<NotificationEntity>(id).ConfigureAwait(false);
            return result?.ToDomain();
        }

        public async Task<Notification> UpdateAsync(Guid id, ApprovalRequest notification)
        {
            var loadData = await _dynamoDbContext.LoadAsync<NotificationEntity>(id).ConfigureAwait(false);
            if (loadData == null) return null;

            if (!string.IsNullOrWhiteSpace(notification.ApprovalNote))
                loadData.AuthorizerNote = notification.ApprovalNote;

            loadData.ApprovalStatus = notification.ApprovalStatus;
            loadData.AuthorizedDate = DateTime.UtcNow;
            await _dynamoDbContext.SaveAsync(loadData).ConfigureAwait(false);

            return loadData.ToDomain();
        }
    }
}
