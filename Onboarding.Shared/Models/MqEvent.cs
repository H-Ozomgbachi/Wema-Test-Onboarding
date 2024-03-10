namespace Onboarding.Shared.Models
{
    public record MqEvent
    {
        public MqEvent(string eventName, string eventBody)
        {
            EventName = eventName;
            EventBody = eventBody;
        }

        public string EventName { get; set; }
        public string EventBody { get; set; }
    }
}
