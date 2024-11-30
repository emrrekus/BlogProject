using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class TagCloud
    {
        [Key]
        public int TagCloudId { get; set; }

        public string Title { get; set; }

        public ICollection<ArticleTag> Articles { get; set; } 

    }
}
