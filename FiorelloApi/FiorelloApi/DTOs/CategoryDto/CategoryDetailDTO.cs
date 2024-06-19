using FiorelloApi.Models;

namespace FiorelloApi.DTOs.CategoryDto
{
    public class CategoryDetailDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
