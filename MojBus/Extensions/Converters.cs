using MojBus.Data.Entities;
using MojBus.Models;
using System.Collections.Generic;

namespace MojBus.Extensions
{
    public static class Converters
    {
        public static List<StopDataModel> StopDataEntityToModel(this List<StopDataEntity> data)
        {
            StopDataModel previous = null;
            List<StopDataModel> mappedData = new List<StopDataModel>();
            foreach (var item in data)
            {
                if (previous == null)
                {
                    previous = new StopDataModel()
                    {
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

        public static List<RouteDataModel> RouteDataToModel(this List<RouteStopsEntity> data)
        {
            return new List<RouteDataModel>();
        }
    }
}
