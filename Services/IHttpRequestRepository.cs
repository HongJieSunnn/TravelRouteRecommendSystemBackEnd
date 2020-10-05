using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelRouteRecommendSystemBackEnd.Model.GetRouteFromCPP;

namespace TravelRouteRecommendSystemBackEnd.Services
{
    /// <summary>
    /// 需要使用http请求其它url从而执行某些操作的功能集合的接口
    /// </summary>
    public interface IHttpRequestRepository
    {
        /// <summary>
        /// 异步获取距离
        /// </summary>
        /// <returns>距离 单位km</returns>
        Task<int> GetDistanceAsync();
        /// <summary>
        /// 获得经纬度的字符串 格式为：纬度,经度
        /// </summary>
        /// <param name="position">地点 一般为城市名</param>
        /// <returns>经纬度的字符串 格式为：纬度,经度</returns>
        Task<string> GetLongitudeAndLatitudeStringAsync(string position);
        /// <summary>
        /// 获得经纬度的字典 其中经度key为lng 纬度key为lat
        /// </summary>
        /// <param name="position"></param>
        /// <returns>经纬度的字典</returns>
        Task<Dictionary<string, double>> GetLongitudeAndLatitudeDictionaryAsync(string position);
        /// <summary>
        /// 获取直线距离
        /// </summary>
        /// <returns>直线距离 单位km</returns>
        Task<double> GetDirectedDistanceAsync();
        /// <summary>
        /// 获取直线距离 重载方法
        /// </summary>
        /// <param name="userRequirement">用户需求 UserRequirementFromCSharp类型</param>
        /// <returns>直线距离 单位km</returns>
        Task<double> GetDirectedDistanceAsync(UserRequirementFromCSharp userRequirement);
    }
}
