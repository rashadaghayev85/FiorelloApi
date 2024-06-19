namespace FiorelloApi.DTOs.BlogDto
{
    public class BlogCreateDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
