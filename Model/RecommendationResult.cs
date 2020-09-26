using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelRouteRecommendSystemBackEnd.Model
{
    public class RecommendationResult
    {
        public List<List<object>> Result { get; set; }
        public string Remark { get; set; }
    }
}
