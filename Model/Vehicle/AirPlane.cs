using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using TravelRouteRecommendSystemBackEnd.Model.GetRouteFromCPP;

namespace TravelRouteRecommendSystemBackEnd.Model
{
    public class AirPlane : Vehicle
    {
		public string PlaneId { get; set; }
		public string PlaneType { get; set; }
		public string StartTime { get; set; }
		public string StartAirport { get; set; }
		public string StartCity { get; set; }
		public string ArrivalTime { get; set; }
		public string ArrivalAirport { get; set; }
		public string ArrivalCity { get; set; }
		public string PuntualityRate { get; set; }
		public string TicketPrice { get; set; }
		public string Discount { get; set; }
		public string Other { get; set; }
		public string CostTime { get; set; }
        public int Mileage { get; set; }
        public AirPlane(
			string vehicleType,
			string planeId,string planeType,
			string startTime,string startAirport,string startCity,
			string arrivalTime,string arrivalAirport,string arrivalCity,
			string puntualityRate,string ticketPrice,string discont,string ohter,string costTime) : base(vehicleType)
        {
			PlaneId = planeId;
			PlaneType = planeType;
			StartTime = startTime;
			StartAirport = startAirport;
			StartCity = startCity;
			ArrivalTime = arrivalTime;
			ArrivalAirport = arrivalAirport;
			ArrivalCity = arrivalCity;
			PuntualityRate = puntualityRate;
			TicketPrice = ticketPrice;
			Discount = discont;
			Other = ohter;
			CostTime = costTime;
		}

		public AirPlane(Route route,double directedDistance) : base(Marshal.PtrToStringAnsi(route.vehicle_type))
        {
			PlaneId = Marshal.PtrToStringAnsi(route.id);
			PlaneType = Marshal.PtrToStringAnsi(route.plane_type);
			StartTime = Marshal.PtrToStringAnsi(route.start_time);
			StartAirport = Marshal.PtrToStringAnsi(route.start_station);
			StartCity = Marshal.PtrToStringAnsi(route.start_city);
			ArrivalTime = Marshal.PtrToStringAnsi(route.arrival_time);
			ArrivalAirport = Marshal.PtrToStringAnsi(route.arrival_station);
			ArrivalCity = Marshal.PtrToStringAnsi(route.arrival_city);
			PuntualityRate = Marshal.PtrToStringAnsi(route.puntuality_rate);
			TicketPrice = Marshal.PtrToStringAnsi(route.ticket_price);
			Discount = Marshal.PtrToStringAnsi(route.discount);
			Other = Marshal.PtrToStringAnsi(route.other);
			CostTime = Marshal.PtrToStringAnsi(route.cost_time);
			Mileage = (int)directedDistance;
		}

        public AirPlane()
        {
        }
    }
}
