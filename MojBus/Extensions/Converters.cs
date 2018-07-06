using MojBus.Data.Entities;
using MojBus.Models;
using System.Collections.Generic;
using System.Linq;

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
    }
}
