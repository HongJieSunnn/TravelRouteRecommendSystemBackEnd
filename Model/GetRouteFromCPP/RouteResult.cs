using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace TravelRouteRecommendSystemBackEnd.Model.GetRouteFromCPP
{
    [StructLayout(LayoutKind.Sequential)]
    public struct RouteResult
    {
        public IntPtr routes;
    }
}
