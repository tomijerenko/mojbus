using Microsoft.EntityFrameworkCore;
using MojBus.Data;
using MojBus.Data.Entities;
using MojBus.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace MojBus.Extensions
{
    public static class MojBusContextExtensions
    {
        public static object GetStops(this MojBusContext context)
        {
            return (from stops in context.Gtfsstops
                   select new
                   {
                       stops.StopId,
                       stops.StopName,
                       stops.StopLat,
                       stops.StopLon,
                       stops.WeelchairBoarding
                   }).OrderBy(x=>x.StopName);
        }

        public static object GetRoutes(this MojBusContext context)
        {
            return (from routes in context.Gtfsroutes
                   select new
                   {
                       routes.RouteId,
                       routes.RouteShortName,
                       routes.RouteLongName
                   }).OrderBy(x => x.RouteShortName);
        }

        public static List<StopDataModel> StopTimesForStop(this MojBusContext context, string stopName, DateTime date)
        {
            //TODO: CHANGE DATE TO CURRENT DATE - data in DB not up to date yet
            object[] sqlParams = {
                new SqlParameter("@StopName", stopName),
                new SqlParameter("@Date", "20180601")
            };

            List<StopDataEntity> data = context.Set<StopDataEntity>().FromSql("exec dbo.TripsWithTimesForStation @StopName, @Date;", sqlParams).ToList();

            return data.StopDataEntityToModel();
        }

        public static List<StopDataModel> StopTimesForStop(this MojBusContext context, string stopName, string routeShortName, DateTime date)
        {
            //TODO: CHANGE DATE TO CURRENT DATE - data in DB not up to date yet
            object[] sqlParams = {
                new SqlParameter("@StopName", stopName),
                new SqlParameter("@RouteShortName", routeShortName),
                new SqlParameter("@Date", "20180601")
            };

            List<StopDataEntity> data = context.Set<StopDataEntity>().FromSql("exec dbo.TripsWithTimesForStationAndRoute @StopName, @RouteShortName, @Date;", sqlParams).ToList();

            return data.StopDataEntityToModel();
        }

        public static List<StopDataModel> StopTimesForStop(this MojBusContext context, string stopName, string routeShortName, string tripHeadSign, DateTime date)
        {
            //TODO: CHANGE DATE TO CURRENT DATE - data in DB not up to date yet
            object[] sqlParams = {
                new SqlParameter("@StopName", stopName),
                new SqlParameter("@RouteShortName", routeShortName),
                new SqlParameter("@TripHeadSign", tripHeadSign),
                new SqlParameter("@Date", "20180601")
            };

            List<StopDataEntity> data = context.Set<StopDataEntity>().FromSql("exec dbo.TripsWithTimesForStationAndRouteTrip @StopName, @RouteShortName, @TripHeadSign, @Date;", sqlParams).ToList();

            return data.StopDataEntityToModel();
        }

        public static List<RouteDataModel> RouteData(this MojBusContext context, string routeShortName, DateTime date)
        {
            //TODO: CHANGE DATE TO CURRENT DATE - data in DB not up to date yet
            object[] sqlParams = {
                new SqlParameter("@RouteShortName", routeShortName),
                new SqlParameter("@Date", "20180601")
            };

            List<RouteStopsEntity> data = context.Set<RouteStopsEntity>().FromSql("exec dbo.RouteStopTimes @RouteShortName, @Date;", sqlParams).ToList();

            return data.RouteDataToModel();
        }
    }
}
