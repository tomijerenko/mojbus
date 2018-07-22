using Microsoft.EntityFrameworkCore;
using MojBus.Data;
using MojBus.Data.Entities;
using MojBus.Models;
using MojBus.Models.FavouriteStops;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace MojBus.Extensions
{
    public static class MojBusContextExtensions
    {
        public static IQueryable<Gtfsstops> GetStops(this MojBusContext context)
        {
            return context.Gtfsstops.OrderBy(x => x.StopName).ThenBy(x => x.StopDirectionId);
        }

        public static List<StopDataModel> StopTimesForStop(this MojBusContext context, string stopName, int directionId, DateTime date)
        {
            object[] sqlParams = {
                new SqlParameter("@StopName", stopName),
                new SqlParameter("@DirectionId", directionId),
                new SqlParameter("@Date", date)
            };

            List<StopDataEntity> data = context
                .Set<StopDataEntity>()
                .FromSql("exec dbo.TripsWithTimesForStationAndTripDirection @StopName, @DirectionId, @Date;", sqlParams)
                .ToList();

            return data
                .StopDataEntityToModel(stopName)
                .GroupByTripShortName()
                .OrderBy(x => x.TripShortName)
                .ToList();
        }

        public static List<StopDataModel> StopTimesForStop(this MojBusContext context, string stopName, string routeShortName, int directionId, DateTime date)
        {
            object[] sqlParams = {
                new SqlParameter("@StopName", stopName),
                new SqlParameter("@RouteShortName", routeShortName),
                new SqlParameter("@DirectionId", directionId),
                new SqlParameter("@Date", date)
            };

            List<StopDataEntity> data = context
                .Set<StopDataEntity>()
                .FromSql("exec dbo.TripsWithTimesForStationAndRouteDirection @StopName, @RouteShortName, @DirectionId, @Date;", sqlParams)
                .AsNoTracking()
                .ToList();

            return data.StopDataEntityToModel(stopName);
        }

        public static List<StopDataModel> StopTimesForStopLoggedIn(this MojBusContext context, string stopName, int directionId, string userId, DateTime date)
        {
            object[] sqlParams = {
                new SqlParameter("@StopName", stopName),
                new SqlParameter("@DirectionId", directionId),
                new SqlParameter("@Date", date)
            };

            List<FavouriteStopRoutes> favouriteStopRoutes = context
                .FavouriteStopRoutes
                .Where(x => x.UserId == userId
                && x.StopName == stopName
                && x.DirectionId == directionId)
                .ToList();
            List<StopDataEntity> data = context
                .Set<StopDataEntity>()
                .FromSql("exec dbo.TripsWithTimesForStationAndTripDirection @StopName, @DirectionId, @Date;", sqlParams)
                .ToList();

            return data
                .StopDataEntityToModel(stopName)
                .GroupByTripShortName()
                .OrderBy(x => x.TripShortName)
                .ToList()
                .AddFavouritesToStops(favouriteStopRoutes);
        }

        public static List<StopTimes> RouteStopTimes(this MojBusContext context, string routeShortName, DateTime date)
        {
            object[] sqlParams = {
                new SqlParameter("@RouteShortName", routeShortName),
                new SqlParameter("@Date", date)
            };

            List<RouteStopsEntity> data = context
                .Set<RouteStopsEntity>()
                .FromSql("exec dbo.RouteStopTimes @RouteShortName, @Date;", sqlParams)
                .ToList();  

            return data
                .RouteStopsEntityToStopTimesModel();
        }

        public static List<StopTimes> RouteStopTimes(this MojBusContext context, string routeShortName, int directionId, DateTime date)
        {
            object[] sqlParams = {
                new SqlParameter("@RouteShortName", routeShortName),
                new SqlParameter("@DirectionId", directionId),
                new SqlParameter("@Date", date)
            };

            List<RouteStopsEntity> data = context
                .Set<RouteStopsEntity>()
                .FromSql("exec dbo.RouteStopTimesForDirectionId @RouteShortName, @DirectionId, @Date;", sqlParams)
                .ToList();

            return data
                .RouteStopsEntityToStopTimesModel();
        }

        public static List<StopDataModel> GetFavouriteStopsLoggedIn(this MojBusContext context, string userId)
        {
            List<FavouriteStopRoutes> favouriteStopRoutes = context
                .FavouriteStopRoutes
                .Where(x => x.UserId == userId)
                .ToList();
            List<StopDataModel> stops = new List<StopDataModel>();
            DateTime date = DateTime.Now;

            if (favouriteStopRoutes.Count() == 0)
                return stops;

            foreach (FavouriteStopRoutes route in favouriteStopRoutes)
            {
                stops.AddRange(context.StopTimesForStop(route.StopName, route.RouteShortName, route.DirectionId, date)
                    .GroupByTripShortName()
                    .ToList());
            }
            stops.ForEach(x => { x.isFavourite = true; });

            return stops;
        }

        public static bool AddStopRouteToFavourites(this MojBusContext context, FavouriteStopRouteModel favouriteStop)
        {
            bool isAdded = false;
            var result = context.FavouriteStopRoutes.Where(x => x.UserId == favouriteStop.UserId
            && x.DirectionId == favouriteStop.DirectionId
            && x.StopName == favouriteStop.StopName
            && x.RouteShortName == favouriteStop.RouteShortName).ToList();

            if (result.Count != 0)
            {
                context.FavouriteStopRoutes.RemoveRange(result);
                isAdded = false;
            }
            else
            {
                context.FavouriteStopRoutes.Add(new FavouriteStopRoutes()
                {
                    UserId = favouriteStop.UserId,
                    RouteShortName = favouriteStop.RouteShortName,
                    StopName = favouriteStop.StopName,
                    DirectionId = favouriteStop.DirectionId
                });
                isAdded = true;
            }
            context.SaveChanges();

            return isAdded;
        }

        public static StopLocationModel GetStopLocation(this MojBusContext context, string stopName, int directionId)
        {
            StopLocationModel model = new StopLocationModel();
            var data = context.Gtfsstops.Where(x => x.StopName == stopName && x.StopDirectionId == directionId).FirstOrDefault();
            if (data != null)
                model = new StopLocationModel() { StopLat = data.StopLat, StopLon = data.StopLon };
            return model;
        }

        //old api methods
        public static IQueryable<Gtfsroutes> GetRoutes(this MojBusContext context)
        {
            return context.Gtfsroutes.OrderBy(x => x.RouteShortName);
        }

        public static List<StopDataModel> StopTimesForStop(this MojBusContext context, string stopName, DateTime date)
        {
            object[] sqlParams = {
                new SqlParameter("@StopName", stopName),
                new SqlParameter("@Date", date)
            };

            List<StopDataEntity> data = context.Set<StopDataEntity>().FromSql("exec dbo.TripsWithTimesForStation @StopName, @Date;", sqlParams).ToList();

            return data.StopDataEntityToModel(stopName);
        }

        public static List<StopDataModel> StopTimesForStop(this MojBusContext context, string stopName, string routeShortName, DateTime date)
        {
            object[] sqlParams = {
                new SqlParameter("@StopName", stopName),
                new SqlParameter("@RouteShortName", routeShortName),
                new SqlParameter("@Date", date)
            };

            List<StopDataEntity> data = context.Set<StopDataEntity>().FromSql("exec dbo.TripsWithTimesForStationAndRoute @StopName, @RouteShortName, @Date;", sqlParams).ToList();

            return data.StopDataEntityToModel(stopName);
        }

        public static List<StopDataModel> StopTimesForStop(this MojBusContext context, string stopName, string routeShortName, string tripHeadSign, DateTime date)
        {
            object[] sqlParams = {
                new SqlParameter("@StopName", stopName),
                new SqlParameter("@RouteShortName", routeShortName),
                new SqlParameter("@TripHeadSign", tripHeadSign),
                new SqlParameter("@Date", date)
            };

            List<StopDataEntity> data = context.Set<StopDataEntity>().FromSql("exec dbo.TripsWithTimesForStationAndRouteTrip @StopName, @RouteShortName, @TripHeadSign, @Date;", sqlParams).ToList();

            return data.StopDataEntityToModel(stopName);
        }
    }
}
