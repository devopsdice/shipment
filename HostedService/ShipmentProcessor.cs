using Newtonsoft.Json;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Shipment.Model;
using Shipment.Service;
using System.Text;

namespace Shipment.HostedService
{
    public class ShipmentProcessor : BackgroundService
    {
        private readonly IShipmentService _shipmentService;
        private IConnection _connection;
        private IModel _channel;
        public ShipmentProcessor(IServiceScopeFactory serviceScopeFactory)
        {
            InitRabbitMQ();
            _shipmentService = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<IShipmentService>();
        }
        private void InitRabbitMQ()
        {

            var factory = new ConnectionFactory
            {
                Uri =
                new Uri("amqp://guest:guest@34.121.163.121:5672/")
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare("OrderCreated_Queue", exclusive: false, durable: true, autoDelete: false);
            _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ch, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                HandleMessage(message);
                _channel.BasicAck(ea.DeliveryTag, false);
            };

            consumer.Shutdown += OnConsumerShutdown;
            consumer.Registered += OnConsumerRegistered;
            consumer.Unregistered += OnConsumerUnregistered;
            consumer.ConsumerCancelled += OnConsumerConsumerCancelled;

            _channel.BasicConsume("OrderCreated_Queue", false, consumer);
            return Task.CompletedTask;
        }
        private void HandleMessage(string content)
        {
            var data = JsonConvert.DeserializeObject<OrderData>(content);
            //Create Shipment Data

            if (data != null && data.OrderId > 0)
            {
                _shipmentService.CreateShipment(new ShipmentData { OrderId = data.OrderId, Status = data.Status });
            }
        }
        private void OnConsumerConsumerCancelled(object sender, ConsumerEventArgs e) { }
        private void OnConsumerUnregistered(object sender, ConsumerEventArgs e) { }
        private void OnConsumerRegistered(object sender, ConsumerEventArgs e) { }
        private void OnConsumerShutdown(object sender, ShutdownEventArgs e) { }
        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e) { }
        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}