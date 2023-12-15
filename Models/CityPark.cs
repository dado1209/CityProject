using ExampleProject.Models.Common;

namespace ExampleProject.Models
{
    public class CityPark: Location
    {
        public required int CityId { get; set; }
        // A park cannot exist without a city
        public City City { get; set; } = null!;
    }
}
