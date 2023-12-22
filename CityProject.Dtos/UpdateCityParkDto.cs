using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityProject.Dtos
{
    // User is only allowed to update city park name and the city to which the park belongs to
    public class UpdateCityParkDto
    {
        [Required]
        [MinLength(1)]
        public string Name { get; set; }

        [Required]
        public int CityId { get; set; }
    }
}
