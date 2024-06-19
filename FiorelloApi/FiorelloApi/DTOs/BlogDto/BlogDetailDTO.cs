namespace FiorelloApi.DTOs.BlogDto
{
    public class BlogDetailDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Image { get; set; }
    }
}
