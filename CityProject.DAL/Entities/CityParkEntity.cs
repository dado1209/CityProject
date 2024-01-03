using CityProject.Models;
using Sieve.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityProject.DAL.Entities
{
    public class CityParkEntity
    {
        public int Id { get; set; }

        [Sieve(CanSort = true, CanFilter = true)]
        public required string Name { get; set; }

        public required int CityId { get; set; }
        // A park cannot exist without a city
        public CityEntity City { get; set; } = null!;
    }
}
