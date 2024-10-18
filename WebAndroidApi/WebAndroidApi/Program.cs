using Bogus;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using WebAndroidApi.Data;
using WebAndroidApi.Data.Entities;
using WebAndroidApi.Interfaces;
using WebAndroidApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<MyStoreContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IImageWorker, ImageWorker>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


string imagesDirPath = Path.Combine(Directory.GetCurrentDirectory(), builder.Configuration["DirImages"]);

if (!Directory.Exists(imagesDirPath))
{
    Directory.CreateDirectory(imagesDirPath);
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(imagesDirPath),
    RequestPath = "/images"
});

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<MyStoreContext>();
    var imageHulk = scope.ServiceProvider.GetRequiredService<IImageWorker>();
    //dbContext.Database.EnsureDeleted();
    dbContext.Database.Migrate();

    if (dbContext.Categories.Count() == 0)
    {
        int number = 10;
        var list = new Faker("uk")
            .Commerce.Categories(number);
        foreach (var name in list)
        {
            string image = imageHulk.Save("https://picsum.photos/1200/800?category").Result;
            var cat = new CategoryEntity
            {
                Name = name,
                Description = new Faker("uk").Commerce.ProductDescription(),
                Image = image
            };
            dbContext.Categories.Add(cat);
            dbContext.SaveChanges();
        }
    }
}

app.Run();
