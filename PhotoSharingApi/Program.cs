using Microsoft.EntityFrameworkCore;
using PhotoSharingApi.DAL;
using PhotoSharingApi.DAL.Models;
using PhotoSharingApi.DAL.Repositories;
using PhotoSharingApi.DAL.Repositories.Interfaces;
using PhotoSharingApi.Services;
using PhotoSharingApi.Services.Interfaces;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

/* TODO (until Weekend)
 *  - finish Album - get album, add album attribute at posting photo & testing
 *  - validation on request and error handling
 *  - refactor
 *  - integrate Alex code (check code, refactor, etc.)
 */

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("Develop"))
);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Dependency Injection
builder.Services.AddTransient<IBaseRepository<Category>, BaseRepository<Category>>();
builder.Services.AddTransient<IPhotoRepository, PhotoRepository>();
builder.Services.AddTransient<IPhotoCategoryRepository, PhotoCategoryRepository>();
builder.Services.AddTransient<IAlbumRepository, AlbumRepository>();
builder.Services.AddTransient<IBaseRepository<PhotoAlbum>, BaseRepository<PhotoAlbum>>();

builder.Services.AddTransient<IPhotoService, PhotoService>();
builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<IAlbumService, AlbumService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
