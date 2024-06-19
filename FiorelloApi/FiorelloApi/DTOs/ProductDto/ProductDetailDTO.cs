namespace FiorelloApi.DTOs.ProductDto
{
    public class ProductDetailDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public List<string> Images { get; set; }
    }
}
