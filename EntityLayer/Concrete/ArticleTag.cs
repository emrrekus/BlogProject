using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class ArticleTag
    {

        public int ArticleId { get; set; }
        public Article Article { get; set; }

        public int TagCloudId { get; set; }
        public TagCloud TagCloud { get; set; }



    }
}
