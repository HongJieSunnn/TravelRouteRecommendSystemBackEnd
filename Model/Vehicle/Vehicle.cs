using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelRouteRecommendSystemBackEnd.CustomComponents;

namespace TravelRouteRecommendSystemBackEnd.Model
{
    public class Vehicle
    {
        public Vehicle(string vehicleType)
        {
            VehicleType=vehicleType;
        }
        public string VehicleType {
            get
            {
                return VehicleType;
            }

            set
            {
                if (value != "HSRC_TYPE" && value != "AIRPLANE_TYPE")
                {
                    throw new CustomExceptionOfHongJieSun(1, "FOUNT_ERROR_VEHICLE_TYPE_WHILE_TRANSMIT_ROUTE_TO_VEHICLE", "由于未知原因在C++返回数据给C#之后在routes中出现了ERROR_TYPE的数据。");
                }
                else
                {
                    VehicleType = value;
                }
            }
        }
    }
}
