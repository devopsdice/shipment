using Shipment.Model;

namespace Shipment.Repository
{
    public interface IShipmentRepository
    {
        Task<int> UpdateShipment(ShipmentData shipmentData);
        Task<ShipmentData> CreateShipment(ShipmentData shipmentData);
    }
}
