using System.ComponentModel.DataAnnotations;

namespace ExampleProject.Dtos
{
    public class CityParkDto
    {
        public int Id { get; set; }
        [Required]
        [MinLength(1)]
        public string Name { get; set; }
        [Required]
        public int CityId { get; set; }
    }
}
