using DataAccess;
using Microsoft.EntityFrameworkCore;
using Services.Abstract;
using Services.Concrete;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddHttpClient();


builder.Services.AddDbContext<Context>(options =>
{
    options.UseSqlite(
        builder.Configuration.GetConnectionString("SqlConnection"),
        b => b.MigrationsAssembly("StokAPI"));  // Migration Assembly'sini belirtiyoruz
});

builder.Services.AddControllers();

builder.Services.AddControllers().AddJsonOptions(x =>
   x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddTransient(typeof(IService<>), typeof(Service<>));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


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

app.Run();
