using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityProject.Models
{
    public class UpdateCityPark
    {
        public string Name { get; set; }

        public int CityId { get; set; }
    }
}
