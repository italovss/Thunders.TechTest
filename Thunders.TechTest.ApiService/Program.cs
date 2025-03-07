using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Thunders.TechTest.ApiService;
using Thunders.TechTest.ApiService.Services;
using Thunders.TechTest.ApiService.Services.Interfaces;
using Thunders.TechTest.Domain.Entities;
using Thunders.TechTest.Domain.Interfaces;
using Thunders.TechTest.Infra.Context;
using Thunders.TechTest.Infra.Persistence;
using Thunders.TechTest.Infra.Repositories;
using Thunders.TechTest.OutOfBox.Database;
using Thunders.TechTest.OutOfBox.Queues;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddControllers();

var features = Features.BindFromConfiguration(builder.Configuration);

// Configura��o do Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Thunders.TechTest API",
        Version = "v1",
        Description = "API para gest�o de pedagios e relat�rios.",
    });
});

// Add services to the container.
builder.Services.AddProblemDetails();

if (features.UseMessageBroker)
{
    builder.Services.AddBus(builder.Configuration, new SubscriptionBuilder()
        .Add<RelatorioRelatorioFaturamentoPorHoraMessage>()
        .Add<RelatorioTopPracasMessage>()
        .Add<RelatorioVeiculosPorPracaMessage>());    
}

if (features.UseEntityFramework)
{
    builder.Services.AddSqlServerDbContext<ApplicationDbContext>(builder.Configuration);

    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    builder.Services.AddScoped<IPedagioRepository, PedagioRepository>();
    builder.Services.AddScoped<IRelatorioFaturamentoRepository, RelatorioFaturamentoRepository>();
    builder.Services.AddScoped<IRelatorioTopPracasRepository, RelatorioTopPracasRepository>();
    builder.Services.AddScoped<IRelatorioVeiculosPorPracaRepository, RelatorioVeiculosPorPracaRepository>();

    builder.Services.AddScoped<IPedagioService, PedagioService>();
    builder.Services.AddScoped<IRelatorioService, RelatorioService>();

    builder.Services.AddScoped<IMessageSender, RebusMessageSender>();
}

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Thunders.TechTest API v1");
        c.RoutePrefix = string.Empty;
    });
}

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

app.MapDefaultEndpoints();

app.MapControllers();

app.Run();
