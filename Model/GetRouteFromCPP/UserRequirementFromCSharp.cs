using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace TravelRouteRecommendSystemBackEnd.Model.GetRouteFromCPP
{
	public interface IUserRequirementFromCSharp
    {

    }

	[StructLayout(LayoutKind.Sequential)]
	public class UserRequirementFromCSharp:IUserRequirementFromCSharp
	{
		public string start_cities;
		public string arrive_cities;
		public int city_num;
		public string start_time;
		public string arrive_time;
		public string travel_type;
		public string vehicle_type;
		public string transit_type;
		public int distances;
		public string remark;
	}
}
