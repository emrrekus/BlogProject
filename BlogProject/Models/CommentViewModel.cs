using EntityLayer.Concrete;

namespace BlogProject.Models
{
    public class CommentViewModel
    {
        public int? UserId { get; set; }

   
        public int ArticleId { get; set; }
        public string Username {  get; set; }
        public string Email { get; set; }
    }
}
