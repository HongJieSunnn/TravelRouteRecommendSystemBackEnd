namespace TravelRouteRecommendSystemBackEnd.Model
{
    public class Vehicle
    {
        public Vehicle()
        {
        }

        public Vehicle(string vehicleType)
        {
            VehicleType = vehicleType;
        }
        public string VehicleType { get; set; }
    }
}
