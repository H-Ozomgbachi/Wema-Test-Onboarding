namespace Onboarding.Tests
{
    public class OnboardingControllerTests
    {

        [Fact]
        public async Task InitiateOnboarding_ReturnsOk()
        {
            // Arrange
            var mockCustomerService = new Mock<ICustomerService>();

            var controller = new OnboardingController(mockCustomerService.Object);

            var payload = new InitiateCustomerOnboardingPayload()
            {
                PhoneNumber = "08144001908",
                Email = "ozomgbachih@gmail.com",
                Password = "Test1234",
                StateOfResidence = "Lagos",
                LocalGovernmentArea = "Shomolu"
            };

            // Act
            var result = await controller.InitiateOnboarding(payload, CancellationToken.None) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task CompleteOnboarding_ReturnsOk()
        {
            // Arrange
            var mockCustomerService = new Mock<ICustomerService>();
            var controller = new OnboardingController(mockCustomerService.Object);
            var payload = new CompleteCustomerOnboardingPayload();

            // Act
            var result = await controller.CompleteOnboarding(payload, CancellationToken.None) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task GetBanks_ReturnsOk()
        {
            // Arrange
            var mockCustomerService = new Mock<ICustomerService>();
            var controller = new OnboardingController(mockCustomerService.Object);

            // Act
            var result = await controller.GetBanks(CancellationToken.None) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task AllOnboardedCustomers_ReturnsOk()
        {
            // Arrange
            var mockCustomerService = new Mock<ICustomerService>();
            var controller = new OnboardingController(mockCustomerService.Object);

            // Act
            var result = await controller.AllOnboardedCustomers(CancellationToken.None) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }
    }
}
