using Microsoft.EntityFrameworkCore;
using PhotoSharingApi.DAL;
using PhotoSharingApi.DAL.Models;
using PhotoSharingApi.DAL.Repositories;
using PhotoSharingApi.DAL.Repositories.Interfaces;
using PhotoSharingApi.Services;
using PhotoSharingApi.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("Develop"))
);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Dependency Injection
builder.Services.AddTransient<IBaseRepository<Category>, BaseRepository<Category>>();

builder.Services.AddTransient<ICategoryService, CategoryService>();

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
