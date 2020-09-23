using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TravelRouteRecommendSystemBackEnd.Data
{
    public class MySQLDbContext:DbContext
    {
        MySQLDbContext(DbContextOptions<MySQLDbContext> options):base(options)
        {

        }
    }
}
