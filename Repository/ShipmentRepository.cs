
using Dapper;
using Shipment.Model;
using ShipmentService.Repository.Base;
using ShipmentService.Repository.Constants;

namespace Shipment.Repository
{
    public class ShipmentRepository : IShipmentRepository
    {
        private readonly IDapperContext _context;

        public ShipmentRepository(IDapperContext context)
        {
            _context = context;
        }

        public async Task<ShipmentData> CreateShipment(ShipmentData shipmentData)
        {
            int result = 0;

            var parameters = new DynamicParameters();
            parameters.Add("@OrderId", shipmentData.OrderId);
            parameters.Add("@Status", shipmentData.Status);

            using (var connection = _context.CreateConnection())
            {
                result = await connection.ExecuteScalarAsync<int>(ShipmentRepositoryConstant.Insert, parameters);
            }
            shipmentData.ShipmentId = result;

            return shipmentData;

        }
        public async Task<int> UpdateShipment(ShipmentData shipmentData)
        {
            int result = 0;

            var parameters = new DynamicParameters();
            parameters.Add("@ShipmentId", shipmentData.ShipmentId);
            parameters.Add("@Status", shipmentData.Status);

            using (var connection = _context.CreateConnection())
            {
                result = await connection.ExecuteScalarAsync<int>(ShipmentRepositoryConstant.Update, parameters);
            }

            return result;
        }
    }
}
