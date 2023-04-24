using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using TheAddress.BLL.Mapping;
using TheAddress.BLL.Services;
using TheAddress.BLL.Services.Interfaces;
using TheAddress.BLL.Validations;
using TheAddress.DAL.Data;
using TheAddress.DAL.Repository;
using TheAddress.DAL.Repository.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();
Log.Logger = new LoggerConfiguration().MinimumLevel.Error().MinimumLevel.Information()
.WriteTo.File("Log/log-.txt", rollingInterval: RollingInterval.Day)
.CreateLogger();
builder.Services.AddDbContext<AppDBContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"));
    opts.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});
builder.Services.AddAutoMapper(typeof(Mapping));
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IGenericService<,>), typeof(GenericService<,>));
builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssembly(typeof(PropertyValidation).Assembly);
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "TheAddress.WebAPI", Version = "v1" });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TheAddress.WebAPI v1"));
//}

app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();

app.Run();
