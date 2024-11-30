using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class SocialMedia
    {
        [Key] 
        public int SocialMediaId { get; set; }

        public string Title { get; set; }
        public string IconUrl { get; set; }
        public string LinkUrl { get; set; }
        public bool Status { get; set; }

        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
