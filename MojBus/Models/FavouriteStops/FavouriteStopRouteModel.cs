using System.ComponentModel.DataAnnotations;

namespace MojBus.Models.FavouriteStops
{
    public class FavouriteStopRouteModel
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string StopName { get; set; }
        [Required]
        public string RouteShortName { get; set; }
        [Required]
        public int DirectionId { get; set; }
    }
}
