using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using TravelRouteRecommendSystemBackEnd.CustomComponents;
using TravelRouteRecommendSystemBackEnd.Model;
using TravelRouteRecommendSystemBackEnd.Model.GetRouteFromCPP;

namespace TravelRouteRecommendSystemBackEnd.Services
{
    public class RecomendationsRepository : IRecommendationsRepository
    {
        private IUserRequirementFromCSharp _userRequirement;
        private string _remark;

        public RecomendationsRepository(IUserRequirementFromCSharp userRequirement)
        {
            _userRequirement = userRequirement;
        }
        public async Task<RecommendationResult> GetRecommendationsAsync()
        {
            return await GetRecommendationRoutesToVehicleObjectsAsync();
        }

        public async Task<RecommendationResult> GetRecommendationRoutesToVehicleObjectsAsync()
        {
            var routes = await GetRouteFromCppAsync();

            return await Task.Run(() =>
            {
                RecommendationResult recommendationResult = new RecommendationResult();
                recommendationResult.Remark = _remark;
                recommendationResult.Result = new List<List<object>>();
                for (int i = 0; i < routes.Count(); i++)
                {
                    int completedRouteSize = routes.ElementAt(i).Count();
                    recommendationResult.Result.Add(new List<object>(completedRouteSize));
                    for (int j = 0; j < completedRouteSize; j++)
                    {
                        var oneRoute = routes.ElementAt(i).ElementAt(j);
                        string vehicleType = Marshal.PtrToStringAnsi(oneRoute.vehicle_type);
                        switch (vehicleType)
                        {
                            case "HSRC_TYPE":
                                recommendationResult.Result[i].Add(new HSRC(oneRoute));
                                break;
                            case "AIRPLANE_TYPE":
                                recommendationResult.Result[i].Add(new AirPlane(oneRoute));
                                break;
                            default:
                                new Vehicle(vehicleType);//用来抛出异常
                                break;
                        }
                    }
                }

                return recommendationResult;
            });
        }

        public async Task<IEnumerable<IEnumerable<Route>>> GetRouteFromCppAsync()
        {
            return await Task.Run(() =>
            {
                int level = 0;
                IntPtr error_code = new IntPtr();
                IntPtr error = new IntPtr();
                IntPtr remark = new IntPtr();
                int route_group_nums = 0;
                int route_size_in_routes = 0;
                IntPtr first_route_of_one_size_array = new IntPtr();
                IntPtr routesFromCpp = new IntPtr();

                List<List<Route>> routes = new List<List<Route>>();
                UserRequirementFromCSharp userRequirement = (UserRequirementFromCSharp)_userRequirement;

                GetRecommendationsOneGroup(userRequirement, ref routesFromCpp, ref level, ref error_code, ref error, ref remark, ref route_group_nums, ref route_size_in_routes, ref first_route_of_one_size_array);

                if (level != 0)//出现了异常了
                {
                    string errorCodeStr = Marshal.PtrToStringAnsi(error_code);
                    string errorMessage = Marshal.PtrToStringAnsi(error);
                    DealExceptionFromCpp(level, errorCodeStr, errorMessage);
                }

                if (remark != null)
                {
                    _remark = Marshal.PtrToStringAnsi(remark);
                }

                int[] first_route_of_one_size_array_managed = new int[route_group_nums];
                if (first_route_of_one_size_array != null)
                {
                    Marshal.Copy(first_route_of_one_size_array, first_route_of_one_size_array_managed, 0, route_group_nums);
                }
                IntPtr[] result = new IntPtr[route_size_in_routes];
                if (routesFromCpp != null)
                {
                    Marshal.Copy(routesFromCpp, result, 0, route_size_in_routes);
                }

                if(result.Length==0)
                {
                    throw new CustomExceptionOfHongJieSun(3, "ROUTE_RESULT_EMPTY", "对不起，该需求暂时没有可推荐的结果");
                }

                int routeReadStartIndex = 0;//开始读取线路的下标
                for (int i = 0; i < route_group_nums; i++)
                {
                    routes.Add(new List<Route>(first_route_of_one_size_array_managed[i]));
                    if (first_route_of_one_size_array_managed[i] == 1)
                    {
                        routes[i].Add(Marshal.PtrToStructure<Route>(result[routeReadStartIndex]));
                        routeReadStartIndex++;
                        continue;
                    }
                    else if (first_route_of_one_size_array_managed[i] == 2)
                    {
                        routes[i].Add(Marshal.PtrToStructure<Route>(result[routeReadStartIndex]));
                        routeReadStartIndex++;
                        routes[i].Add(Marshal.PtrToStructure<Route>(result[routeReadStartIndex]));
                        routeReadStartIndex++;
                        continue;
                    }
                    else
                    {
                        throw new CustomExceptionOfHongJieSun(1, "ERROR_SIZE_OF_ONE_ROUTE", $"first_route_of_one_size_array_managed[{i}]={first_route_of_one_size_array_managed[i]}错误的大小，只有1或2正确");
                    }
                }

                FreeMemoryOneGroup(ref routesFromCpp, ref first_route_of_one_size_array, route_size_in_routes);

                return routes;
            });
        }

        [DllImport(@"G:\Sourse\TravelRouteRecommendSystemDLL\x64\Debug\TravelRouteRecommendSystemDLL.dll", EntryPoint = "GetRecommendationsOneGroup", CallingConvention = CallingConvention.Cdecl)]
        private static extern void GetRecommendationsOneGroup(
            UserRequirementFromCSharp reqirement,
            ref IntPtr routes,
            ref int level,
            ref IntPtr error_code,
            ref IntPtr error,
            ref IntPtr remark,
            ref int route_group_nums,
            ref int route_size_in_routes,
            ref IntPtr first_route_of_one_size_array);

        [DllImport(@"G:\Sourse\TravelRouteRecommendSystemDLL\x64\Debug\TravelRouteRecommendSystemDLL.dll", EntryPoint = "FreeMemoryOneGroup", CallingConvention = CallingConvention.Cdecl)]
        private static extern void FreeMemoryOneGroup(ref IntPtr routes, ref IntPtr first_route_of_one_size_array, int route_size_in_routes);

        private void DealExceptionFromCpp(int level, string error_code, string error)
        {
            switch (level)
            {
                case 1://一级的错误 都是算法错误 记录到日志里 对用户发送statue:500 遇到了无法解决的错误 请联系工作人员
                case 2://二级的错误 都是输入错误 也记录到日志里 对用户发送statue:400 输入的参数错误(或其它难以预料的错误)
                case 3://三级错误 算不上真的错误 一般是没有数据 回200+提示就好了
                case 4://未知的错误
                    throw new CustomExceptionOfHongJieSun(level, error_code, error);
                default:
                    throw new CustomExceptionOfHongJieSun(1, "ERROR_LEVEL_ERROR", "由于未知原因出现了错误的ERROR_LEVEL");
            }
        }
    }
}
