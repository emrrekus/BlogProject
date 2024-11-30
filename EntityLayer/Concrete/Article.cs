using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Article
    {
        [Key]
        public int ArticleId { get; set; }
        public string Title { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CoverImageUrl { get; set; }
        public string MainImageUrl { get; set; }
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public string Detail { get; set; }
        public int? ArticleViewCount { get; set; }

        public ICollection<Comment> Comments { get; set; } 
        public ICollection<ArticleTag> Tags { get; set; }


    }
}
