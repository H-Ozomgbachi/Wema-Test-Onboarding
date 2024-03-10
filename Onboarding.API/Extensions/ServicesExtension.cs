using Onboarding.API.Consumers;

namespace Onboarding.API.Extensions
{
    public static class ServiceConfigurationExtensions
    {
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo { Title = "WEMA ALAT - Customer Onboarding", Version = "v1" });
            });
        }
        public static void ConfigureVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new QueryStringApiVersionReader("api-version"),
                    new HeaderApiVersionReader("X-Version"),
                    new MediaTypeApiVersionReader("api-version"));
            });
        }
        public static void ConfigureAppServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitWork, UnitOfWork>();

            services.AddScoped<ICustomerService, CustomerService>();
        }
        public static void ConfigureOtherServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddOptions<AppSettings>().Bind(config.GetSection("AppSettings"));
            services.AddOptions<RabbitMqSettings>().Bind(config.GetSection("RabbitMqSettings"));
            services.AddAutoMapper(typeof(CustomerMappings).Assembly);
            services.AddSingleton<CryptographyHelper>();

            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(config["AppSettings:DbConnection"]), ServiceLifetime.Singleton);

            services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssemblyContaining<InitiateCustomerOnboardingPayloadValidator>();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddScoped<IRabbitMqService, RabbitMqService>();
            services.AddScoped<ICacheService, CacheService>();
            services.AddScoped<IRestClientService, RestClientService>();
        }
        public static void ConfigureRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(config =>
            {
                config.AddConsumer<OTPConsumer>();
                config.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(new Uri(configuration["RabbitMqSettings:RootUri"]), host =>
                    {
                        host.Username(configuration["RabbitMqSettings:Username"]);
                        host.Password(configuration["RabbitMqSettings:Password"]);
                    });

                    cfg.ReceiveEndpoint(configuration["RabbitMqSettings:QueueName"], e =>
                    {
                        e.ConfigureConsumer<OTPConsumer>(context);
                    });
                });
            });
        }
    }
}
