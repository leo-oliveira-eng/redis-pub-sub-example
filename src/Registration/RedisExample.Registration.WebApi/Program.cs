using MediatR;
using Microsoft.EntityFrameworkCore;
using RedisExample.Registration.Persistence.Context;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddHttpContextAccessor();

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

var registrationConnectionString = builder.Configuration.GetConnectionString("Registration");
var serverVersion = ServerVersion.AutoDetect(registrationConnectionString);

builder.Services.AddDbContext<RegistrationContext>(options => options.UseMySql(registrationConnectionString, serverVersion));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbucklew7
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
