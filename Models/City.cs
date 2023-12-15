using ExampleProject.Models.Common;

namespace ExampleProject.Models
{
    public class City: Location
    {
        public DateTime LastUpdatedOn { get; set; }

        public ICollection<CityPark> Parks { get;} = new List<CityPark>();

    }
}
