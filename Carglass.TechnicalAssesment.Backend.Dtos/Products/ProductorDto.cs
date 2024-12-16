namespace Carglass.TechnicalAssessment.Backend.Dtos.Products
{
    public class ProductoDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int ProductType { get; set; }
        public long NumTerminal { get; set; }
        public DateTime SoldAt { get; set; }
    }
}
