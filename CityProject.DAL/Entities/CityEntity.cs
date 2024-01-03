using Sieve.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityProject.DAL.Entities
{
    public class CityEntity
    {
        public int Id { get; set; }

        [Sieve(CanSort = true, CanFilter = true)]
        public required string Name { get; set; }

        public DateTime LastUpdatedOn { get; set; }

        public ICollection<CityParkEntity> Parks { get; } = new List<CityParkEntity>();
    }
}
