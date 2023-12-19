using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityProject.Dtos
{
    public class UpdateCityParkDto
    {
        [Required]
        [MinLength(1)]
        public string Name { get; set; }

        [Required]
        public int CityId { get; set; }
    }
}
