namespace Carglass.TechnicalAssessment.Backend.Entities.Entities
{
    public class Producto
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int ProductType { get; set; }
        public long NumTerminal { get; set; }
        public DateTime SoldAt { get; set; }
    }
}
