using off_tone.Application.Interfaces.Repositories.BlogPostRepos;
using off_tone.Application.Interfaces.Repositories.BlogRepos;
using off_tone.Persistence.Extensions;
using off_tone.Persistence.Seeds;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.RegisterPersistenceServices(builder.Configuration);

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
    var services = scope.ServiceProvider;

    try
    {
        var blogPostWriteRepository = services.GetService<IBlogPostWriteRepository>();
        var blogWriteRepository = services.GetService<IBlogWriteRepository>();
        await DbInitializer.seedBlogPosts(blogPostWriteRepository, blogWriteRepository);
    }
    catch(Exception ex)
    {
        Console.WriteLine("Exception occured while trying to get a service in Program.cs " + ex.Message);
        throw new Exception(ex.Message);
    }
}

app.Run();
