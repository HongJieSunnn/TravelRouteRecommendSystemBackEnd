using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TravelRouteRecommendSystemBackEnd.CustomComponents;
using TravelRouteRecommendSystemBackEnd.Model.GetRouteFromCPP;

namespace TravelRouteRecommendSystemBackEnd.Services
{
    /// <summary>
    /// 实现了IHttpRequestRepository的地图api请求相关函数类
    /// </summary>
    public class HttpRequestMapRepository : IHttpRequestRepository
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly UserRequirement _userRequirement;
        public HttpRequestMapRepository(IHttpClientFactory httpClientFactory,IUserRequirementFromCSharp userRequirement)
        {
            _httpClient = httpClientFactory;
            _userRequirement = (UserRequirement)userRequirement;
        }
        public async Task<double> GetDirectedDistanceAsync()
        {
            var startCityLngAndLatTask = GetLongitudeAndLatitudeDictionaryAsync(_userRequirement.StartCities);
            var arrivalCityLngAndLatTask = GetLongitudeAndLatitudeDictionaryAsync(_userRequirement.ArriveCities);

            var startCityLngAndLat = await startCityLngAndLatTask;
            var arrivalCityLngAndLat = await arrivalCityLngAndLatTask;

            double R = 6371;//地球半径

            return Math.Acos(Math.Sin((Math.PI/180)* startCityLngAndLat["lat"]) * Math.Sin((Math.PI / 180) * arrivalCityLngAndLat["lat"]) +
                Math.Cos((Math.PI / 180) * startCityLngAndLat["lat"]) * Math.Cos((Math.PI / 180) * arrivalCityLngAndLat["lat"]) *
                Math.Cos((Math.PI / 180) * arrivalCityLngAndLat["lng"] - (Math.PI / 180) * startCityLngAndLat["lng"])) * R;
        }

        public async Task<double> GetDirectedDistanceAsync(UserRequirementFromCSharp userRequirement)
        {
            var startCityLngAndLatTask =GetLongitudeAndLatitudeDictionaryAsync(userRequirement.start_cities);
            var arrivalCityLngAndLatTask =GetLongitudeAndLatitudeDictionaryAsync(userRequirement.arrive_cities);

            var startCityLngAndLat = await startCityLngAndLatTask;
            var arrivalCityLngAndLat = await arrivalCityLngAndLatTask;

            double R = 6371;//地球半径

            return Math.Acos(Math.Sin((Math.PI / 180) * startCityLngAndLat["lat"]) * Math.Sin((Math.PI / 180) * arrivalCityLngAndLat["lat"]) +
                Math.Cos((Math.PI / 180) * startCityLngAndLat["lat"]) * Math.Cos((Math.PI / 180) * arrivalCityLngAndLat["lat"]) *
                Math.Cos((Math.PI / 180) * arrivalCityLngAndLat["lng"] - (Math.PI / 180) * startCityLngAndLat["lng"])) * R;
        }

        public async Task<int> GetDistanceAsync()
        {
            var startCityLngAndLatTask = GetLongitudeAndLatitudeStringAsync(_userRequirement.StartCities);
            var arrivalCityLngAndLatTask = GetLongitudeAndLatitudeStringAsync(_userRequirement.ArriveCities);
            var startCityLngAndLat = await startCityLngAndLatTask;
            var arrivalCityLngAndLat = await arrivalCityLngAndLatTask;

            var request = new HttpRequestMessage(HttpMethod.Get, $"http://api.map.baidu.com/routematrix/v2/driving?output=json&origins={startCityLngAndLat}&destinations={arrivalCityLngAndLat}&tactics=10&ak=FbB269nBtfw1SmdgaNQXAXFhNeMXNzo8");
            var client = _httpClient.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseStr = await response.Content.ReadAsStringAsync();
                var responseObject = JObject.Parse(responseStr);

                return (int)responseObject["result"][0]["distance"]["value"].ToObject<double>() / 1000;
            }
            else
            {
                throw new CustomExceptionOfHongJieSun(1, "ERROR_WHEN_HTTP_REQUEST_TO_GET_DISTANCE", $"获取{_userRequirement.StartCities}-{_userRequirement.ArriveCities}距离失败");
            }
        }

        public async Task<Dictionary<string, double>> GetLongitudeAndLatitudeDictionaryAsync(string position)
        {
            Dictionary<string, double> lngAndLat = new Dictionary<string, double>();
            var request = new HttpRequestMessage(HttpMethod.Get, $"http://api.map.baidu.com/geocoding/v3/?address={position}&output=json&ak=FbB269nBtfw1SmdgaNQXAXFhNeMXNzo8");
            var client = _httpClient.CreateClient();

            var response = await client.SendAsync(request);
            return await Task.Run(async () =>
            {
                if (response.IsSuccessStatusCode)
                {
                    var responseStr = await response.Content.ReadAsStringAsync();
                    var responseObject = JObject.Parse(responseStr);

                    lngAndLat["lng"] = responseObject["result"]["location"]["lng"].ToObject<double>();
                    lngAndLat["lat"] = responseObject["result"]["location"]["lat"].ToObject<double>();

                    return lngAndLat;
                }
                else
                {
                    throw new CustomExceptionOfHongJieSun(1, "ERROR_WHEN_HTTP_REQUEST_TO_GET_LONGITUDE_AND_LATITUDE_DICTIONARY", $"获取{position}经纬度字典失败");
                }
            });
        }

        public async Task<string> GetLongitudeAndLatitudeStringAsync(string position)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"http://api.map.baidu.com/geocoding/v3/?address={position}&output=json&ak=FbB269nBtfw1SmdgaNQXAXFhNeMXNzo8");
            var client = _httpClient.CreateClient();

            var response = await client.SendAsync(request);
            return await Task.Run(async () =>
            {
                if (response.IsSuccessStatusCode)
                {
                    var responseStr = await response.Content.ReadAsStringAsync();
                    var responseObject = JObject.Parse(responseStr);

                    return $"{responseObject["result"]["location"]["lat"]},{responseObject["result"]["location"]["lng"]}";
                }
                else
                {
                    throw new CustomExceptionOfHongJieSun(1, "ERROR_WHEN_HTTP_REQUEST_TO_GET_LONGITUDE_AND_LATITUDE_STRING", $"获取{position}经纬度字符串失败");
                }
            });
        }
    }
}
