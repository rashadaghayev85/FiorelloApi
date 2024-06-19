namespace FiorelloApi.DTOs.BlogDto
{
    public class BlogEditDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile ImageFile { get; set; }
        public string Image { get; set; }
    }
}
