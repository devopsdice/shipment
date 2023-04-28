using Shipment.Model;

namespace Shipment.MessageBroker
{
    public interface IMessageBrokerService
    {
        void PublishMessage(ShipmentData orderData);
    }
}
