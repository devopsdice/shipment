namespace Shipment.MessageBroker
{
    using Newtonsoft.Json;
    using Shipment.MessageBroker;
    using Shipment.Model;
    using RabbitMQ.Client;
    using System.Text;

    public class RabbitMQService: IMessageBrokerService
    {
        private ConnectionFactory connectionFactory;
        private IConnection connection;
        private IModel model;
        public RabbitMQService()
        {
            connectionFactory = new ConnectionFactory
            {
                Uri =
                new Uri("amqp://guest:guest@34.121.163.121:5672/")
            };
            connection = connectionFactory.CreateConnection();
            model = connection.CreateModel();
            //model.QueueDeclare("Shipment_Queue", exclusive: false, durable: true, autoDelete: false);


        }
        public void PublishMessage(ShipmentData  shipmentData)
        {
            
            model.BasicPublish("Order_Status", "",null, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(shipmentData)));
        }

    }
}
