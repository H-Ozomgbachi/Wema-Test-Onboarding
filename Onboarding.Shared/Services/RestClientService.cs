namespace Onboarding.Shared.Services
{
    public interface IRestClientService
    {
        Task<T> GetAsync<T>(string url, IDictionary<string, string> headers = default);
    }

    public class RestClientService : IRestClientService
    {
        private readonly ILogger<RestClientService> _logger;

        public RestClientService(ILogger<RestClientService> logger)
        {
            _logger = logger;
        }

        public async Task<T> GetAsync<T>(string url, IDictionary<string, string> headers)
        {
            T objResp = default;
            RestResponse response = new();
            try
            {
                var options = new RestClientOptions()
                {
                    RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true,
                };

                var client = new RestClient(options);
                var request = new RestRequest(url, Method.Get);

                request.AddHeader("Content-Type", GeneralConstants.ApplicationJson);

                if (headers != null)
                {
                    foreach (KeyValuePair<string, string> header in headers)
                    {
                        if (header.Key.Trim() == "Authorization")
                        {
                            options.Authenticator = new JwtAuthenticator(header.Value);
                            request.AddHeader(header.Key, value: header.Value);
                            continue;
                        }
                    }
                }
                response = await client.ExecuteAsync(request);

                _logger.LogInformation($"RestClient call response - {url}, {response.Content}\n");
                objResp = UtilityHelper.DeSerializer<T>(response.Content);
            }
            catch (Exception ex)
            {
                _logger.LogError($"RestClient error::: {ex.Message}\n {nameof(GetAsync)}\n Payload::: {UtilityHelper.Serializer(url)}\n HttpResponse::: {UtilityHelper.Serializer(response)} \n");
            }
            return objResp;
        }
    }
}
