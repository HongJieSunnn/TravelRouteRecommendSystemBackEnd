using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace TravelRouteRecommendSystemBackEnd.Model.GetRouteFromCPP
{
	[StructLayout(LayoutKind.Sequential)]
	public struct Route
	{
		public IntPtr vehicle_type;
		public IntPtr id;
		public IntPtr plane_type;
		public IntPtr start_time;
		public IntPtr start_station;
		public IntPtr start_city;
		public IntPtr arrival_time;
		public IntPtr arrival_station;
		public IntPtr arrival_city;
		public IntPtr puntuality_rate;
		public IntPtr ticket_price;
		public IntPtr discount;
		public IntPtr other;
		public IntPtr cost_time;
		public IntPtr start_type;
		public IntPtr arrival_type;
		public IntPtr second_class_seat_price;
		public IntPtr one_class_seat_price;
		public IntPtr business_seat_price;
		public IntPtr mileage;
	}
}
