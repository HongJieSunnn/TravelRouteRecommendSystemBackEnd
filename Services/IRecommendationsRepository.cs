using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelRouteRecommendSystemBackEnd.Model;
using TravelRouteRecommendSystemBackEnd.Model.GetRouteFromCPP;

namespace TravelRouteRecommendSystemBackEnd.Services
{
    interface IRecommendationsRepository
    {
        /// <summary>
        /// 获取推荐信息
        /// </summary>
        /// <param name="userRequirement">用户需求</param>
        /// <returns>
        /// 返回多个路线推荐结果
        /// </returns>
        Task<RecommendationResult> GetRecommendationsAsync();
        /// <summary>
        /// 把从C++中获得Route对象转换成Vehicle对象。
        /// </summary>
        /// <param name="userRequirement">从GetRecommendationsAsync传入的用户需求</param>
        /// <returns>返回Vehicle对象的集合</returns>
        Task<RecommendationResult> GetRecommendationRoutesToVehicleObjectsAsync();
        /// <summary>
        /// 从C++中获取数据
        /// </summary>
        /// <param name="userRequirement"></param>
        /// <returns>Route对象的集合 其中的成员都是IntPtr类型的 需要GetRecommendationRoutesToVehicleObjectsAsync转换</returns>
        Task<IEnumerable<IEnumerable<Route>>> GetRouteFromCppAsync();
    }
}
