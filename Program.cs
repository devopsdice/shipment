using Shipment.HostedService;
using Shipment.MessageBroker;
using Shipment.Repository;
using Shipment.Service;
using ShipmentService.Repository.Base;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IDapperContext, DapperContext>();
builder.Services.AddScoped<IShipmentRepository, ShipmentRepository>();
builder.Services.AddScoped<IMessageBrokerService, RabbitMQService>();
builder.Services.AddScoped<IShipmentService, Shipment.Service.ShipmentService>();
builder.Services.AddControllers();
builder.Services.AddHostedService<ShipmentProcessor>();
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
