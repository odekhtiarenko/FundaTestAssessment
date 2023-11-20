using FundaTestAssessment.Api.AutomapperProfile;
using FundaTestAssessment.Api.RetryPoliciesConfiguration;
using FundaTestAssessment.Domain.EstateApiClient;
using FundaTestAssessment.Domain.EstateApiClient.Models;
using FundaTestAssessment.Domain.QueryHandlers;
using FundaTestAssessment.Domain.Services;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(typeof(GetTopActiveRealEstateAgentsQueryHandler).Assembly);
builder.Services.AddAutoMapper(typeof(MapperProfile));

builder.Services.AddTransient<IEstateApiClient, EstateApiClient>();
builder.Services.AddTransient<IMessageSender, MessageSender>();

var options = builder.Configuration
                     .GetSection("ApiClientConfiguration")
                     .Get<ApiClientConfiguration>()!;

builder.Services.AddHttpClient(ApiClientConfiguration.ApiClientName, c =>
{
    c.BaseAddress = new Uri($"{options.BaseUrl}/{options.ApiKey}/");
}).AddRetryPolicies();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

public partial class Program { }
