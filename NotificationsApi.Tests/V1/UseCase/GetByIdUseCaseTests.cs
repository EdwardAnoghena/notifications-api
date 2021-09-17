using NotificationsApi.V1.Gateways;
using NotificationsApi.V1.UseCase;
using Moq;


namespace NotificationsApi.Tests.V1.UseCase
{
    public class GetByIdUseCaseTests
    {
        private Mock<INotificationGateway> _mockGateway;
        private GetByIdNotificationCase _classUnderTest;


        public GetByIdUseCaseTests()
        {
            _mockGateway = new Mock<INotificationGateway>();
            _classUnderTest = new GetByIdNotificationCase(_mockGateway.Object);
        }

        //TODO: test to check that the use case retrieves the correct record from the database.
        //Guidance on unit testing and example of mocking can be found here https://github.com/LBHackney-IT/lbh-base-api/wiki/Writing-Unit-Tests
    }
}
