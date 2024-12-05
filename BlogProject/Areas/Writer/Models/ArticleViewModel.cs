using EntityLayer.Concrete;

namespace BlogProject.Areas.Writer.Models
{
    public class ArticleViewModel
    {
        public int ArticleId { get; set; }
        public string Title { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public DateTime CreatedDate { get; set; }
        public IFormFile CoverImageUrl { get; set; }
        public IFormFile MainImageUrl { get; set; }
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public string Detail { get; set; }
        public int? ArticleViewCount { get; set; }

    }
}
