using MojBus.Models;
using System.Collections.Generic;
using System.Linq;

namespace MojBus.Extensions
{
    public static class CustomLINQExtensions
    {
        public static List<TransitTime> OrderTransitTimesByDepartureTimeAscending(this IEnumerable<TransitTime> times)
        {
            List<TransitTime> orderedTransitTimes = times.OrderBy(x => x.DepartureTime).ToList();
            List<TransitTime> temp = times.TakeWhile(x => x.DepartureTime.Contains("+")).ToList();
            foreach (TransitTime time in temp)
            {
                orderedTransitTimes.Remove(time);
            }
            orderedTransitTimes.AddRange(temp);

            return orderedTransitTimes;
        }
    }
}
