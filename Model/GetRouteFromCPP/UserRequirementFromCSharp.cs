using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using TravelRouteRecommendSystemBackEnd.Services;

namespace TravelRouteRecommendSystemBackEnd.Model.GetRouteFromCPP
{
    enum TravelTypeEnum
	{
		商务出差,个人出游, 朋友出游
    }
	public interface IUserRequirementFromCSharp
	{

	}
	
	public class UserRequirement:IUserRequirementFromCSharp
    {
		[Required]
        [StringLength(maximumLength: 6,MinimumLength = 1,ErrorMessage ="起始城市名需在1-6个字之间" )]
		public string StartCities { get; set; }
		[Required]
		[StringLength(maximumLength: 6, MinimumLength = 1, ErrorMessage = "到达城市名需在1-6个字之间")]
		public string ArriveCities { get; set; }
		[Required]
		[Range(1,1,ErrorMessage ="城市组数只能为1")]
		public int CityNum { get; set; }
		[Required]
		[StringLength(maximumLength: 16, MinimumLength = 16,ErrorMessage = "格式必须为yyyy-mm-dd hh:mm 长度不够")]
		public string StartTime { get; set; }
		[Required]
		[StringLength(maximumLength: 16, MinimumLength = 16, ErrorMessage = "格式必须为yyyy-mm-dd hh:mm 长度不够")]
		public string ArriveTime { get; set; }
		[Required]
		public string TravelType { get; set; }
		[Required]
		public string VehicleType { get; set; }
		[Required]
		public string TransitType { get; set; }
		[Required]
		[Range(0,20000)]
		public int Distances { get; set; }
		[StringLength(maximumLength:35)]
		public string Remark { get; set; }

		public async Task<UserRequirementFromCSharp> UserRequirementToUserRequirementFromCSharp(IHttpClientFactory httpClientFactory)
        {
			UserRequirementFromCSharp userRequirementFromCSharp = new UserRequirementFromCSharp();
			userRequirementFromCSharp.start_cities = StartCities;
			userRequirementFromCSharp.arrive_cities = ArriveCities;
			userRequirementFromCSharp.city_num = CityNum;
			userRequirementFromCSharp.start_time = StartTime;
			userRequirementFromCSharp.arrive_time = ArriveTime;
			userRequirementFromCSharp.travel_type = TravelType;
			userRequirementFromCSharp.vehicle_type = VehicleType;
			userRequirementFromCSharp.transit_type = TransitType;
			if(Distances==0)
            {
				Distances = await new HttpRequestMapRepository(httpClientFactory, this).GetDistanceAsync();
            }
			userRequirementFromCSharp.distances = Distances;
			userRequirementFromCSharp.remark = Remark;

			return userRequirementFromCSharp;
		}

        public override string ToString()
        {
			return $"{{StartCities:{StartCities} \n" +
				$"ArriveCities:{ArriveCities} \n" +
				$"CityNum:{CityNum} \n" +
				$"StartTime:{StartTime} \n" +
				$"ArriveTime:{ArriveTime} \n" +
				$"TravelType:{TravelType} \n" +
				$"VehicleType:{VehicleType} \n" +
				$"TransitType:{TransitType} \n" +
				$"Distances:{Distances} \n" +
				$"Remark:{Remark}}}\n";
        }
    }

	[StructLayout(LayoutKind.Sequential)]
	public class UserRequirementFromCSharp : IUserRequirementFromCSharp
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
