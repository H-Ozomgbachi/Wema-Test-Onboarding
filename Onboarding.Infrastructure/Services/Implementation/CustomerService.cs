namespace Onboarding.Infrastructure.Services.Implementation
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitWork _unitWork;
        private readonly IMapper _mapper;
        private readonly IRestClientService _restClientService;
        private readonly ICacheService _cacheService;
        private readonly IRabbitMqService _rabbitMqService;
        private readonly CryptographyHelper _cryptography;
        private readonly AppSettings _appSettings;
        private readonly RabbitMqSettings _rabbitMqSettings;

        public CustomerService(IUnitWork unitWork, IMapper mapper, IRestClientService restClientService, IOptions<AppSettings> options, ICacheService cacheService, IRabbitMqService rabbitMqService, IOptions<RabbitMqSettings> options1, CryptographyHelper cryptography)
        {
            _unitWork = unitWork;
            _mapper = mapper;
            _restClientService = restClientService;
            _cacheService = cacheService;
            _rabbitMqService = rabbitMqService;
            _cryptography = cryptography;
            _appSettings = options.Value;
            _rabbitMqSettings = options1.Value;
        }

        public async Task<BaseResponse<string>> InitiateCustomerOnboarding(InitiateCustomerOnboardingPayload createCustomerPayload, CancellationToken cancellationToken)
        {
            Customer customer = _mapper.Map<Customer>(createCustomerPayload);

            string otp = UtilityHelper.GetRandomDigits(6);

            OnboardingOtpModel otpModel = new()
            {
                Otp = otp,
                Email = customer.Email,
                Expires = DateTimeOffset.Now.AddSeconds(60)
            };

            string cacheKey = string.Concat(createCustomerPayload.PhoneNumber.Trim(), '-', otp);

            // Mask password before caching on REDIS
            customer.Password = _cryptography.EncryptString(customer.Password);

            await _cacheService.SetCache(cacheKey, UtilityHelper.Serializer(customer), TimeSpan.FromSeconds(60));

            await _rabbitMqService.PublishToQueue(_rabbitMqSettings.QueueName, new MqEvent("OTP", UtilityHelper.Serializer(otpModel)));

            return new BaseResponse<string>
            {
                ResponseData = "A one-time-password has been sent to you for verification"
            };
        }

        public async Task<BaseResponse<List<BankModel>>> GetAllBanks(CancellationToken cancellationToken)
        {
            List<BankModel> result;

            string cachedBanks = await _cacheService.GetFromCache("Banks");

            if (string.IsNullOrEmpty(cachedBanks))
            {
                RestResult<List<BankModel>> data = await _restClientService.GetAsync<RestResult<List<BankModel>>>(_appSettings.AllBanksUrl);

                result = data.result;

                await _cacheService.SetCache("Banks", UtilityHelper.Serializer(result), TimeSpan.FromHours(1));
            }
            else
            {
                result = UtilityHelper.DeSerializer<List<BankModel>>(cachedBanks);
            }

            return new BaseResponse<List<BankModel>>
            {
                ResponseData = result
            };
        }

        public async Task<BaseResponse<List<CustomerModel>>> GetCustomers(CancellationToken cancellationToken)
        {
            IEnumerable<Customer> customers = await _unitWork.CustomerRepository.GetAll(cancellationToken);

            return new BaseResponse<List<CustomerModel>>
            {
                ResponseData = _mapper.Map<List<CustomerModel>>(customers)
            };
        }

        public async Task<BaseResponse<string>> CompleteCustomerOnboarding(CompleteCustomerOnboardingPayload completeCustomerOnboarding, CancellationToken cancellationToken)
        {
            string cacheKey = string.Concat(completeCustomerOnboarding.PhoneNumber.Trim(), '-', completeCustomerOnboarding.Otp.Trim());

            string cachedValue = await _cacheService.GetFromCache(cacheKey);

            if (string.IsNullOrEmpty(cachedValue))
            {
                throw new BadRequestException("Invalid or expired OTP");
            }

            Customer customer = UtilityHelper.DeSerializer<Customer>(cachedValue);

            // Un-mask password retrieved from REDIS cache for proper hashing before saving on customer DB
            customer.Password = _cryptography.DecryptString(customer.Password);

            HashHelper.CreateHashAndSalt(customer.Password, out string securityStamp, out string passwordHash);

            customer.Password = passwordHash;
            customer.SecurityStamp = securityStamp;

            bool isOnboarded = await _unitWork.CustomerRepository.Exists(c => c.PhoneNumber == customer.PhoneNumber || c.Email == customer.Email, cancellationToken);

            if (isOnboarded)
            {
                throw new BadRequestException("This customer has already been onboarded");
            }

            _unitWork.CustomerRepository.Create(customer);

            await _unitWork.SaveChanges(cancellationToken);

            return new BaseResponse<string>
            {
                ResponseData = "Customer onboarding successful"
            };
        }
    }
}
