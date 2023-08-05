using off_tone.Application.Extensions;
using off_tone.Application.Interfaces.Repositories.BlogPostRepos;
using off_tone.Application.Interfaces.Repositories.BlogRepos;
using off_tone.Application.Interfaces.Repositories.ReviewRepos;
using off_tone.Application.Interfaces.Repositories.TagRepos;
using off_tone.Persistence.Extensions;
using off_tone.Persistence.Seeds;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.RegisterApplicationServices();
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
        var tagWriteRepository = services.GetService<ITagWriteRepository>();
        var reviewWriteRepository = services.GetService<IReviewWriteRepository>();

        await DbInitializer.seedBlogPosts(blogPostWriteRepository, blogWriteRepository, tagWriteRepository, reviewWriteRepository);
    }
    catch(Exception ex)
    {
        Console.WriteLine("Exception occured while trying to get a service in Program.cs " + ex.Message);
        throw new Exception(ex.Message);
    }
}

app.Run();
