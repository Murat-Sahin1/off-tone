using BlogService.Persistence.Data;
using BlogService.Persistence.Extensions;
using BlogService.Persistence.Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var currentDb =  builder.Services.RegisterDbContext(builder.Environment);
Console.WriteLine("--> Current Db: " + currentDb);

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

var isSuccess = await DbInitializer.PrepDb(app, SD.IsProd);
if (!isSuccess) return;

app.Run();
