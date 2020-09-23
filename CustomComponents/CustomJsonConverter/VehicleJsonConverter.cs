using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using TravelRouteRecommendSystemBackEnd.Model;

namespace TravelRouteRecommendSystemBackEnd.CustomComponents.CustomJsonConverter
{
    public class VehicleJsonConverter : CustomJsonConverterOfHongJieSun<Vehicle>
    {
        protected override Vehicle Create(Type objectType, JObject jObject)
        {
            if (jObject == null) throw new ArgumentNullException("jObject is null");

            return null;
        }
    }
}
