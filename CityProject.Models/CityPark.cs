using CityProject.Models.Common;

namespace CityProject.Models
{
    public class CityPark : Location
    {
        public required int CityId { get; set; }
        // A park cannot exist without a city
        public City City { get; set; } = null!;
    }
}
