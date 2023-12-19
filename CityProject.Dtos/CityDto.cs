using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityProject.Dtos
{
    public class CityDto
    {
        public int Id { get; set; }
        /* data validation */
        [Required]
        [MinLength(1)]
        public required string Name { get; set; }

    }
}
