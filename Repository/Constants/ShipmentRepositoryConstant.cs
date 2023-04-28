namespace ShipmentService.Repository.Constants
{
    public static class ShipmentRepositoryConstant
    {
        public const string Insert = @"INSERT INTO public.""Shipment"" (""OrderId"",""Status"")VALUES(@OrderId,@Status) RETURNING ""ShipmentId"";";

        public const string Update = @"UPDATE public.""Shipment"" SET ""Status""=@Status WHERE ""ShipmentId""=@ShipmentId;";
    }
}
