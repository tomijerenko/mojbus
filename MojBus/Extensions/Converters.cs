using MojBus.Data.Entities;
using MojBus.Models;
using System.Collections.Generic;
using System.Linq;

namespace MojBus.Extensions
{
    public static class Converters
    {
        /// <summary>
        /// Maps table like list StopDataEntity into object like representation StopDataModel.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="stopName"></param>
        /// <returns></returns>
        public static List<StopDataModel> StopDataEntityToModel(this List<StopDataEntity> data, string stopName)
        {
            StopDataModel previous = null;
            List<StopDataModel> mappedData = new List<StopDataModel>();
            foreach (var item in data)
            {
                if (previous == null)
                {
                    previous = new StopDataModel()
                    {
                        StopName = stopName,
                        TripShortName = item.TripShortName,
                        TripHeadsign = item.TripHeadsign,
                        DirectionID = item.DirectionID,
                        RouteId = item.RouteId,
                        DepartureTimes = new List<string>() { item.DepartureTime.Trim() }
                    };
                }
                else if (item.TripHeadsign != previous.TripHeadsign ||
                    item.TripShortName != previous.TripShortName ||
                    item.DirectionID != previous.DirectionID)
                {
                    mappedData.Add(previous);
                    previous = new StopDataModel()
                    {
                        StopName = stopName,
                        TripShortName = item.TripShortName,
                        TripHeadsign = item.TripHeadsign,
                        DirectionID = item.DirectionID,
                        RouteId = item.RouteId,
                        DepartureTimes = new List<string>() { item.DepartureTime.Trim() }
                    };
                }
                else
                    previous.DepartureTimes.Add(item.DepartureTime.Trim());
            }
            mappedData.Add(previous);

            return mappedData;
        }

        /// <summary>
        /// Groups list of stops by their trip short name, merges and sorts their stop times.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static List<StopDataModel> GroupByTripShortName(this List<StopDataModel> data)
        {
            if (data.First() == null)
                return new List<StopDataModel>();

            List<StopDataModel> temp = new List<StopDataModel>();
            StopDataModel previous = null;

            foreach (string distinctedTripShortName in data.Select(x=>x.TripShortName).Distinct())
            {
                foreach (StopDataModel item in data.Where(x=>x.TripShortName == distinctedTripShortName))
                {
                    if (previous == null)
                    {
                        previous = item;
                        continue;
                    }
                    previous.DepartureTimes.AddRange(item.DepartureTimes);
                }
                previous.DepartureTimes = previous.DepartureTimes.OrderBy(x=>x).ToList();
                temp.Add(previous);
                previous = null;
            }

            return temp;
        }

        public static List<StopDataModel> AddFavouritesToStops(this List<StopDataModel> data, List<FavouriteStopRoutes> favourites)
        {
            foreach (StopDataModel stopData in data)
            {
                foreach (FavouriteStopRoutes route in favourites)
                {
                    if (stopData.TripShortName == route.RouteShortName)
                    {
                        stopData.isFavourite = true;
                    }
                }
            }

            return data;
        }
    }
}
