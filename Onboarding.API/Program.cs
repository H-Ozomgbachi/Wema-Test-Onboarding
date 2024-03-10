WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

//Logger Setup
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

// Add services to the container
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidationExceptionFilter>();
    options.SuppressAsyncSuffixInActionNames = false;
})
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    });

string[] origins = builder.Configuration["AppSettings:AllowedOrigins"].Split(';');
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder => builder
    .WithOrigins(origins)
    .AllowAnyMethod().AllowAnyHeader());
});

builder.Services.ConfigureRabbitMq(builder.Configuration);
builder.Services.ConfigureAppServices();
builder.Services.ConfigureSwagger();
builder.Services.ConfigureVersioning();
builder.Services.ConfigureOtherServices(builder.Configuration);

builder.Host.UseSerilog();

WebApplication app = builder.Build();

// Request pipelines registry
app.UseSwagger();
app.UseSwaggerUI(s =>
{
    s.SwaggerEndpoint("/swagger/v1/swagger.json", "");
});

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.UseMiddleware<GlobalExceptionMiddleware>();
app.MapControllers();

app.Run();