using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityProject.Dtos
{
    public class UpdateCityDto
    {
        [Required]
        public required string Name { get; set; }
    }
}
