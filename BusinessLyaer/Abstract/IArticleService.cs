using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLyaer.Abstract
{
    public interface IArticleService : IGenericService<Article>
    {
        Task<List<Article>> TArticleListWithCategoryAndAppUserbyUserIdAsync(int id);
        List<Article> TArticleListWithCategoryAndAppUser();
        Article TArticleListWithCategoryAndAppUserByArticleId(int id);
        Task<Article> TArticleListWithCategoryAndAppUserByArticleIdAsync(int id);
        Task TArticleViewCountIncreaseAsync(int id);

        Task<List<Comment>> TCommentListWithArticleId(int id);

       
    }
}
