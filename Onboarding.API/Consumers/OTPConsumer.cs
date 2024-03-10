namespace Onboarding.API.Consumers
{
    public class OTPConsumer : IConsumer<MqEvent>
    {
        public OTPConsumer()
        {
        }

        // This consumer is to MOCK a mobile phone that receives OTP
        public async Task Consume(ConsumeContext<MqEvent> context)
        {
            try
            {
                OnboardingOtpModel data = UtilityHelper.DeSerializer<OnboardingOtpModel>(context.Message.EventBody);

                Console.WriteLine("---- My iPhone 14 Pro Max ------");
                Console.WriteLine("");

                Console.WriteLine("Sending One-Time-Password...");
                Console.WriteLine("");
                await Task.Delay(TimeSpan.FromMilliseconds(10));

                Console.WriteLine($"Dear {data.Email}, your one time password is {data.Otp}\nIt expires at {data.Expires.ToString("dd-MMM-yyy hh:mm:ss tt")}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing message: {ex.Message}");
            }
        }
    }
}
