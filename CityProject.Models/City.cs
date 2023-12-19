using CityProject.Models.Common;

namespace CityProject.Models
{
    public class City : Location
    {
        public DateTime LastUpdatedOn { get; set; }

        public ICollection<CityPark> Parks { get; } = new List<CityPark>();
    }
}
