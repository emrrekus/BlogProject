using EntityLayer.Concrete;
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
        Article ArticleListWithCategoryAndAppUserByArticleId(int id);
        Task<Article> ArticleListWithCategoryAndAppUserByArticleIdAsync(int id);
        Task ArticleViewCountIncreaseAsync(int id);

       Task<List<Comment>>  CommentListWithArticleId(int id);
    }
}
