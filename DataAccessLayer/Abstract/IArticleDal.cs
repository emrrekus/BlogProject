using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IArticleDal : IGenericDal<Article>
    {
        List<Article> ArticleListWithCategoryAndAppUser();
      
        Task<List<Article>> ArticleListWithCategoryAndAppUserbyUserIdAsync(int id);
        Article ArticleListWithCategoryAndAppUserByArticleId(int id);
        Task<Article> ArticleWithCategoryAndAppUserByArticleIdAsync(int id);
        Task ArticleViewCountIncreaseAsync(int id);

       Task<List<Comment>>  CommentListWithArticleId(int id);


       Task CreateArticle(Article article, int UserId, int[] tagId, IFormFile CoverImage, IFormFile MainImage);

        Task UpdateArticleWithTagsAsync(Article article, int[] tagId, IFormFile? CoverImage, IFormFile? MainImage);

        Task<string> SaveFileAsync(IFormFile file, string uploadFolder);

        void EnsureDirectoryExists(string path);


    }
}
