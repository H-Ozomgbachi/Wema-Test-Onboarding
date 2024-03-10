namespace Onboarding.Shared.Services
{
    public interface IRabbitMqService
    {
        Task PublishToQueue(string queueName, MqEvent mqEvent);
    }
    public class RabbitMqService : IRabbitMqService
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public RabbitMqService(ISendEndpointProvider sendEndpointProvider)
        {
            _sendEndpointProvider = sendEndpointProvider;
        }

        public async Task PublishToQueue(string queueName, MqEvent mqEvent)
        {
            var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{queueName}"));
            await endpoint.Send(mqEvent);
        }
    }
}
