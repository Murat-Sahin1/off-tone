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
builder.Services.AddAuthorization();

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

DbInitializer.PrepDb(app);

app.Run();
