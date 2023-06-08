namespace BarmenYachting.Application.DTO
{
    public class VesselDto
    {
        public string ManufacterName { get; set; }
        public string Type { get; set; }
        public string Model { get; set; }
        public decimal Price { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Length { get; set; }
    }

    public class CreateVesselDto
    {
        public string Model { get; set; }
        public decimal Price { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Length { get; set; }
        public int ManufacterId { get; set; }
        public int TypeId { get; set; }
    }
}
