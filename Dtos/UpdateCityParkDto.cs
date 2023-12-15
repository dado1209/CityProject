using System.ComponentModel.DataAnnotations;

namespace ExampleProject.Dtos
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
