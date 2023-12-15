using System.ComponentModel.Design;
using AuthService.EventProcessing;
using AuthService.Extensions.Identity;
using AuthService.Infrastructure.Data;
using AuthService.Infrastructure.Data.Identity.Contexts;
using AuthService.Infrastructure.Data.Identity.Entities;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registering Identity services
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.RegisterIdentityServices(builder.Configuration);

// Event Processor
builder.Services.AddSingleton<IEventProcessor, EventProcessor>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppIdentityDbContext>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

    try
    {
        await DbInitializer.PrepDb(dbContext);
        await DbInitializer.SeedUsersAsync(userManager);
    }
    catch(Exception ex)
    {
        Console.WriteLine($"--> An error occured during migration: {ex.Message}");
    }
}

app.Run();