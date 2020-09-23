using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using TravelRouteRecommendSystemBackEnd.Model.GetRouteFromCPP;

namespace TravelRouteRecommendSystemBackEnd.Model
{
    public class HSRC : Vehicle
    {
		public string CarId { get; set; }
		public string StartType { get; set; }
		public string StartTime { get; set; }
		public string StartStation { get; set; }
		public string StartCity { get; set; }
		public string CostTime { get; set; }
		public string ArrivalType { get; set; }
		public string ArrivalTime { get; set; }
		public string ArrivalStation { get; set; }
		public string ArrivalCity { get; set; }
		public string SecondClassSeatPrice { get; set; }
		public string OneClassSeatPrice { get; set; }
		public string BusinessSeatPrice { get; set; }
		public string Mileage { get; set; }
		public HSRC(
			string vehicleType,
			string carId, string startType,string arrivalType,
			string startTime, string startStation, string startCity,
			string arrivalTime, string arrivalStation, string arrivalCity,
			string secondClassSeatPrice, string oneClassSeatPrice, string businessSeatPrice, string mileage, string costTime) :base(vehicleType)
        {
			CarId = carId;
			StartType = startType;
			StartTime = startTime;
			StartStation = startStation;
			StartCity = startCity;
			CostTime = costTime;
			ArrivalType = arrivalType;
			ArrivalTime = arrivalTime;
			ArrivalStation = arrivalCity;
			SecondClassSeatPrice = secondClassSeatPrice;
			OneClassSeatPrice = oneClassSeatPrice;
			BusinessSeatPrice = businessSeatPrice;
			Mileage = mileage;
		}

		public HSRC(Route route) : base(Marshal.PtrToStringAnsi(route.vehicle_type))
        {
			CarId = Marshal.PtrToStringAnsi(route.id);
			StartType = Marshal.PtrToStringAnsi(route.start_type);
			StartTime = Marshal.PtrToStringAnsi(route.start_time);
			StartStation = Marshal.PtrToStringAnsi(route.start_station);
			StartCity = Marshal.PtrToStringAnsi(route.start_city);
			CostTime = Marshal.PtrToStringAnsi(route.cost_time);
			ArrivalType = Marshal.PtrToStringAnsi(route.arrival_type);
			ArrivalTime = Marshal.PtrToStringAnsi(route.arrival_time);
			ArrivalStation = Marshal.PtrToStringAnsi(route.arrival_station);
			SecondClassSeatPrice = Marshal.PtrToStringAnsi(route.second_class_seat_price);
			OneClassSeatPrice = Marshal.PtrToStringAnsi(route.one_class_seat_price);
			BusinessSeatPrice = Marshal.PtrToStringAnsi(route.business_seat_price);
			Mileage = Marshal.PtrToStringAnsi(route.mileage);
		}
    }
}
