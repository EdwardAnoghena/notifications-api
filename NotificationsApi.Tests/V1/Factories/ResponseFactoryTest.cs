using NotificationsApi.V1.Domain;
using NotificationsApi.V1.Factories;
using Xunit;

namespace NotificationsApi.Tests.V1.Factories
{
    public class ResponseFactoryTest
    {
        //TODO: add assertions for all the fields being mapped in `ResponseFactory.ToResponse()`. Also be sure to add test cases for
        // any edge cases that might exist.
        [Fact]
        public void CanMapADatabaseEntityToADomainObject()
        {
            var domain = new Notification();
            var response = domain.ToResponse();
            //TODO: check here that all of the fields have been mapped correctly. i.e. response.fieldOne.Should().Be("one")
        }
    }
}
