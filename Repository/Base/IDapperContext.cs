using System.Data;

namespace ShipmentService.Repository.Base
{
    public interface IDapperContext
    {
        IDbConnection CreateConnection();
    }
}
