using Shipment.Model;

namespace Shipment.Service
{
    public interface IShipmentService
    {
        void UpdateShipment(ShipmentData  shipmentData);
        void CreateShipment(ShipmentData shipmentData);
    }
}
