using System.Text.Json.Serialization;
using CS.Data;
using CS.Data.Models;
using CS.RequestResponse.Client;
using CS.RequestResponse.Courier;
using CS.Services;
using CS.Services.Interfaces;
using CS.Services.Mapper;
using Microsoft.EntityFrameworkCore;
using Repo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    })
    .AddNewtonsoftJson();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped(typeof(IRepository<>),typeof(Repository<>));
builder.Services.AddScoped<IMapper<CreateClientResponse, CreateClientRequest, Client>, ClientMapper>();
builder.Services.AddScoped<IMapper<CreateCourierResponse, CreateCourierRequest, Courier>, CourierMapper>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<ICourierService,CourierService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderHistoryService, OrderHistoryService>();
builder.Services.AddDbContext<DataContext>(options=>options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();