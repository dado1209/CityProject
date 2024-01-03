
using Sieve.Attributes;

namespace CityProject.Models.Common
{
    public abstract class Location
    {
        public int Id { get; set; }

        [Sieve(CanSort = true, CanFilter = true)]
        public required string Name { get; set; }
    }
}
