using System.ComponentModel.DataAnnotations;

namespace ExampleProject.Dtos

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
