namespace ExampleProject.Models.Common
{
    public abstract class Location
    {
        public int Id { get; set; }
        public required string Name { get; set; }
    }
}
