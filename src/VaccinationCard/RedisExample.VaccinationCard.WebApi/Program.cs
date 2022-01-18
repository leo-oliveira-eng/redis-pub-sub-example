using MediatR;
using RedisExample.VaccinationCard.Common.Settings;
using RedisExample.VaccinationCard.CrossCutting.DI.Extensions;
using RedisExample.VaccinationCard.WebApi.Controllers.Default;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<VaccinationCardSettings>(builder.Configuration.GetSection("ApiSettings"));

builder.Services.ConfigureDependencyInjector();

builder.Services.AddHttpContextAccessor();

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

builder.Services.AddMediatR(typeof(Controller));

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
