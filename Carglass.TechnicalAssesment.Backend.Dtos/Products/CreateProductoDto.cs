namespace Carglass.TechnicalAssessment.Backend.Dtos
{
    public class CreateProductoDto
    {
        public string ProductName { get; set; } = string.Empty;
        public int ProductType { get; set; }
        public long NumTerminal { get; set; }
        public DateTime SoldAt { get; set; }
    }
}
