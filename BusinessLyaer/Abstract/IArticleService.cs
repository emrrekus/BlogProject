using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLyaer.Abstract
{
    public interface IArticleService : IGenericService<Article>
    {
        Task TCreateArticle(Article article, int UserId, int[] tagId, IFormFile CoverImage, IFormFile MainImage);
        Task<List<Article>> TArticleListWithCategoryAndAppUserbyUserIdAsync(int id);
        List<Article> TArticleListWithCategoryAndAppUser();
        Article TArticleListWithCategoryAndAppUserByArticleId(int id);
        Task<Article> TArticleWithCategoryAndAppUserByArticleIdAsync(int id);
        Task TArticleViewCountIncreaseAsync(int id);

        Task<List<Comment>> TCommentListWithArticleId(int id);

        Task TUpdateArticleWithTagsAsync(Article article, int[] tagId, IFormFile? CoverImage, IFormFile? MainImage);


    }
}
