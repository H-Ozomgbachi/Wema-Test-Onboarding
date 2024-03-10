namespace Onboarding.API.Controllers.v1
{
    public class OnboardingController : BaseControllerV1
    {
        private readonly ICustomerService _customerService;

        public OnboardingController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost("initiate-onboarding")]
        public async Task<IActionResult> InitiateOnboarding([FromBody]InitiateCustomerOnboardingPayload payload, CancellationToken cancellationToken)
        {
            var response = await _customerService.InitiateCustomerOnboarding(payload, cancellationToken);
            return Ok(response);
        }

        [HttpPost("complete-onboarding")]
        public async Task<IActionResult> CompleteOnboarding([FromBody] CompleteCustomerOnboardingPayload payload, CancellationToken cancellationToken)
        {
            var response = await _customerService.CompleteCustomerOnboarding(payload, cancellationToken);
            return Ok(response);
        }

        [HttpGet("get-banks")]
        public async Task<IActionResult> GetBanks(CancellationToken cancellationToken)
        {
            var response = await _customerService.GetAllBanks(cancellationToken);
            return Ok(response);
        }

        [HttpGet("all-onboarded-customers")]
        public async Task<IActionResult> AllOnboardedCustomers(CancellationToken cancellationToken)
        {
            var response = await _customerService.GetCustomers(cancellationToken);
            return Ok(response);
        }
    }
}
