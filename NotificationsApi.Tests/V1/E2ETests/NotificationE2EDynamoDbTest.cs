using AutoFixture;
using NotificationsApi;
using NotificationsApi.Tests;
using NotificationsApi.V1.Domain;
using NotificationsApi.V1.Factories;
using NotificationsApi.V1.Infrastructure;
using FluentAssertions;
using System;
using System.Net;
using System.Threading.Tasks;
using NotificationsApi.V1.Boundary.Request;
using Xunit;
using NotificationsApi.V1.Boundary.Response;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Net.Http.Headers;
using NotificationsApi.V1.Common.Enums;

namespace NotificationsApi.Tests.V1.E2ETests
{
 //   For guidance on writing integration tests see the wiki page https://github.com/LBHackney-IT/lbh-base-api/wiki/Writing-Integration-Tests
 //   Example integration tests using DynamoDb

public class NotificationE2EDynamoDbTest : DynamoDbIntegrationTests<Startup>
    {
        private readonly Fixture _fixture = new Fixture();

        /// <summary>
        /// Method to construct a test entity that can be used in a test
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private Notification ConstructTestEntity()
        {
            var entity = _fixture.Create<Notification>();
            entity.CreatedAt = DateTime.UtcNow;
            return entity;
        }
        private NotificationRequest GivenANewNotificationRequest(TargetType targetType= TargetType.FailedDirectDebits, string message = "Direct Debit failed")
        {
            var entity = _fixture.Build<NotificationRequest>()
                .With(_=>_.TargetType, targetType)
                 .With(_ => _.Message, message)
                .Create();
            return entity;
        }
        private NotificationRequest GivenANewNotificationRequestWithValidationErrors()
        {
            var entity = _fixture.Build<NotificationRequest>()
                           .Without(_=>_.TargetId)
                           .Without(_=>_.TargetType)
                           .Create();
            return entity;
        }

        /// <summary>
        /// Method to add an entity instance to the database so that it can be used in a test.
        /// Also adds the corresponding action to remove the upserted data from the database when the test is done.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private async Task SetupTestData(Notification entity)
        {
            await DynamoDbContext.SaveAsync(entity.ToDatabase()).ConfigureAwait(false);
            CleanupActions.Add(async () => await DynamoDbContext.DeleteAsync<NotificationEntity>(entity.TargetId).ConfigureAwait(false));
        }

        [Fact]
        public async Task GetEntityByTargetIdNotFoundReturns404()
        {
            var targetId = Guid.NewGuid();
            var uri = new Uri($"api/v1/notifications/{targetId}", UriKind.Relative);
            var response = await Client.GetAsync(uri).ConfigureAwait(false);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetNotificationByTargetdIdFoundReturnsResponse()
        {
            var entity = ConstructTestEntity();
            await SetupTestData(entity).ConfigureAwait(false);
            var uri = new Uri($"api/v1/notifications/{entity.TargetId}", UriKind.Relative);
            var response = await Client.GetAsync(uri).ConfigureAwait(false);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var apiEntity = JsonSerializer.Deserialize<NotificationResponseObject>(responseContent, CreateJsonOptions());

            apiEntity.Should().BeEquivalentTo(entity, (x) => x.Excluding(y => y.CreatedAt));
            apiEntity.CreatedDate.Date.Should().BeCloseTo(DateTime.UtcNow.Date);
        }

        [Fact]
        public async Task PostNotificationReturnsCreated()
        {
            var requestObject = GivenANewNotificationRequest();

            var uri = new Uri($"api/v1/notifications", UriKind.Relative);
            string body = JsonSerializer.Serialize(requestObject, CreateJsonOptions());

            using StringContent stringContent = new StringContent(body);
            stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await Client.PostAsync(uri, stringContent).ConfigureAwait(false);

            response.StatusCode.Should().Be(HttpStatusCode.Created);



            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var apiNotification = JsonSerializer.Deserialize<NotificationResponseObject>(responseContent, CreateJsonOptions());

            apiNotification.TargetId.Should().NotBeEmpty();
            var dbRecord = await DynamoDbContext.LoadAsync<NotificationEntity>(apiNotification.TargetId).ConfigureAwait(false);
            apiNotification.TargetId.Should().Be(dbRecord.TargetId);
            requestObject.TargetType.Should().Be(dbRecord.TargetType);
            requestObject.Message.Should().BeEquivalentTo(dbRecord.Message);
        }

        protected static JsonSerializerOptions CreateJsonOptions()
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            options.Converters.Add(new JsonStringEnumConverter());
            return options;
        }

        [Fact]
        public async Task PostNotificationReturnsBadRequestWithValidationErrors()
        {
            var requestObject = GivenANewNotificationRequestWithValidationErrors();

            var uri = new Uri($"api/v1/notifications", UriKind.Relative);
            string body = JsonSerializer.Serialize(requestObject, CreateJsonOptions());

            using StringContent content = new StringContent(body);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await Client.PostAsync(uri, content).ConfigureAwait(false);

            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            JObject jo = JObject.Parse(responseContent);
            var errors = jo["errors"].Children();

            ShouldHaveErrorFor(errors, "FirstName");
            ShouldHaveErrorFor(errors, "Surname");
            ShouldHaveErrorFor(errors, "MiddleName");
            ShouldHaveErrorFor(errors, "PlaceOfBirth");

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        }

        private static void ShouldHaveErrorFor(JEnumerable<JToken> errors, string propertyName, string errorCode = null)
        {
            var error = errors.FirstOrDefault(x => (x.Path.Split('.').Last().Trim('\'', ']')) == propertyName) as JProperty;
            error.Should().NotBeNull();
            if (!string.IsNullOrEmpty(errorCode))
                error.Value.ToString().Should().Contain(errorCode);
        }
    }
}
