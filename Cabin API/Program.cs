using Cabin_API.AppMapping;
using Cabin_API.MassTransit.Consumers;
using Cabin_API.Services;
using Cabin_API.Services.DataServices;
using MassTransit;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// AutoMappper
builder.Services.AddAutoMapper(typeof(AppMappingService));

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo()
    {
        Title = "Cabin API",
        Description = "Cabin API (ASP.NET Core 6.0)",
        TermsOfService = new Uri("https://github.com/openapitools/openapi-generator"),
        Contact = new OpenApiContact
        {
            Name = "Yurii",
            Email = "frolyakyurii@gmail.com"
        },
        Version = "v1",
    });
});

builder.Services.AddSingleton<MongoDbConnectionService>();
builder.Services.AddSingleton<CabinService>();
builder.Services.AddSingleton<PriceService>();
builder.Services.AddSingleton<PromocodeService>();

builder.Services.AddMassTransit(options =>
{
    options.AddConsumer<GetPriceConsumer>();
    options.AddConsumer<GetCabinsCountconsumer>();
    options.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("cabin-api", false));

    options.UsingRabbitMq((context, config) =>
    {
        var host = builder.Configuration.GetSection("RabbitMq:Host").Get<string>();

        config.Host(host, h =>
        {
            h.Username(builder.Configuration.GetSection("RabbitMq:Username").Get<string>());
            h.Password(builder.Configuration.GetSection("RabbitMq:Password").Get<string>());
        });

        config.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
