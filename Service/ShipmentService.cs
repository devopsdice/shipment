using Shipment.MessageBroker;
using Shipment.Model;
using Shipment.Repository;

namespace Shipment.Service
{
    public class ShipmentService: IShipmentService
    {
        private IShipmentRepository _shipmentRepository;
        private IMessageBrokerService _messageBrokerService;
        public ShipmentService(IShipmentRepository  shipmentRepository, IMessageBrokerService messageBrokerService)
        {
            _shipmentRepository = shipmentRepository;
            _messageBrokerService = messageBrokerService;
        }

        public void CreateShipment(ShipmentData shipmentData)
        {
            _shipmentRepository.CreateShipment(shipmentData);
        }

        public void UpdateShipment(ShipmentData  shipmentData)
        {
            _shipmentRepository.UpdateShipment (shipmentData);
            _messageBrokerService.PublishMessage(shipmentData);
        }
    }
}
