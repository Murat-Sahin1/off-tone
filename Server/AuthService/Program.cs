using AuthService.Data;
using AuthService.Data.Identity.Contexts;
using AuthService.Data.Identity.Entities;
using AuthService.Extensions.Identity;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.RegisterIdentityServices(builder.Configuration);

builder.Services.AddIdentityCore<AppUser>(opt =>
{

}).AddEntityFrameworkStores<AppIdentityDbContext>()
  .AddSignInManager<SignInManager<AppUser>>();

builder.Services.AddAuthentication();
builder.Services.AddAuthorizationCore();

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