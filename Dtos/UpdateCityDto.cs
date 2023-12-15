using System.ComponentModel.DataAnnotations;

namespace ExampleProject.Dtos
{
    public class UpdateCityDto
    {
        [Required]
        public required string Name { get; set; }
    }
}
