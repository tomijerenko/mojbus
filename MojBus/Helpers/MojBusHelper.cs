using Microsoft.EntityFrameworkCore;
using MojBus.Data;
using MojBus.Data.Entities;
using MojBus.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace MojBus.Helpers
{
    public class MojBusHelper
    {
        public static object GetStops(MojBusContext context)
        {
            return from stops in context.Gtfsstops
                   select new
                   {
                       stops.StopId,
                       stops.StopName,
                       stops.StopLat,
                       stops.StopLon,
                       stops.WeelchairBoarding
                   };
        }

        public static List<StopDataModel> StopTimesForStop(MojBusContext context, int stopId, DateTime date)
        {
            //TODO: CHANGE DATE TO CURRENT DATE - data in DB not up to date yet
            object[] sqlParams = {
                new SqlParameter("@StopId", stopId),
                new SqlParameter("@Date", "20180601")
            };

            List<StopDataEntity> data = context.Set<StopDataEntity>().FromSql("exec dbo.TripsWithTimesForStation @StopId, @Date;", sqlParams).ToList();

            return Converters.StopDataEntityToModel(data);
        }

        public static List<StopDataModel> StopTimesForStop(MojBusContext context, int stopId, int routeId, DateTime date)
        {
            //TODO: CHANGE DATE TO CURRENT DATE - data in DB not up to date yet
            object[] sqlParams = {
                new SqlParameter("@StopId", stopId),
                new SqlParameter("@RouteId", routeId),
                new SqlParameter("@Date", "20180601")                
            };

            List<StopDataEntity> data = context.Set<StopDataEntity>().FromSql("exec dbo.TripsWithTimesForStationAndRoute @StopId, @RouteId, @Date;", sqlParams).ToList();

            return Converters.StopDataEntityToModel(data);
        }

        public static object GetRoutes(MojBusContext context)
        {
            return from routes in context.Gtfsroutes
                   select new
                   {
                       routes.RouteId,
                       routes.RouteShortName,
                       routes.RouteLongName
                   };
        }
    }
}
