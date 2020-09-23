using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelRouteRecommendSystemBackEnd.CustomComponents
{
    public class CustomExceptionOfHongJieSun:Exception
    {

        public CustomExceptionOfHongJieSun(int level,string errorCode,string what):base(what)
        {
            Data["level"] = level;
            Data["error_code"] = errorCode;
        }
    }
}
