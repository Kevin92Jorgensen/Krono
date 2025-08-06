using Krono.Infrastructure.Data;
using Krono.Infrastructure.Repositories;
using Krono.Infrastructure.Services;
using Krono.IntegrationServices;
using Krono.IntegrationServices.Bilka;
using Krono.IntegrationServices.Coop;
using Krono.IntegrationServices.Fotex;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<KronoDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("MySqlConnection"),
        new MySqlServerVersion(new Version(8, 0, 36)) // Update to your MySQL version
    ));

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IShopService, ShopService>();

builder.Services.AddScoped<NettoImporter>();
builder.Services.AddHttpClient<NettoImporter>();
builder.Services.AddScoped<BilkaImporter>();
builder.Services.AddHttpClient<CoopImporter>();
builder.Services.AddScoped<CoopImporter>();
//builder.Services.AddHttpClient<BilkaImporter>();
builder.Services.AddScoped<FotexImporter>();
//builder.Services.AddHttpClient<FotexImporter>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("https://saverr.netlify.app/") // React dev server
              .AllowAnyHeader()
              .AllowAnyOrigin()
              .AllowAnyMethod();
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowFrontend"); // ?? enable it

app.UseAuthorization();

app.MapControllers();

app.Run();
