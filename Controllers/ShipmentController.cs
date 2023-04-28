using Microsoft.AspNetCore.Mvc;
using Shipment.Model;
using Shipment.Service;

namespace Shipment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipmentController : ControllerBase
    {
        private IShipmentService _shipmentService;
        public ShipmentController(IShipmentService shipmentService)
        {
            _shipmentService = shipmentService;
        }
        [HttpPost]
        public void post([FromBody] ShipmentData shipmentData)
        {
            _shipmentService.UpdateShipment(shipmentData);

        }
    }
}
