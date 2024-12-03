using DataAccessLayer.Abstract;
using DataAccessLayer.Context;
using DataAccessLayer.Repositories;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfArticleDal : GenericRepository<Article>, IArticleDal
    {
        private BlogContext _blogContext;
        public EfArticleDal(BlogContext blogContext) : base(blogContext)
        {
            _blogContext = blogContext;
        }

        public List<Article> ArticleListWithCategoryAndAppUser()
        {
            return _blogContext.Articles
        .Include(x => x.Category)
        .Include(x => x.AppUser)
        .ToList();
        }

        public Article ArticleListWithCategoryAndAppUserByArticleId(int id)
        {
            var values = _blogContext.Articles
        .Where(x => x.ArticleId == id)
        .Include(x => x.Category)
        .Include(x => x.AppUser)
        .Include(x => x.Tags)
        .ThenInclude(t => t.TagCloud)
        .FirstOrDefault();

            return values;
        }

        public async Task<Article> ArticleListWithCategoryAndAppUserByArticleIdAsync(int id)
        {
            var values = await _blogContext.Articles
      .Where(x => x.ArticleId == id)
      .Include(x => x.Category)
      .Include(x => x.AppUser)
      .Include(x => x.Tags)
          .ThenInclude(t => t.TagCloud)
      .FirstOrDefaultAsync();

            return  values;

        }

        public async Task ArticleViewCountIncreaseAsync(int id)
        {
            var values = await _blogContext.Articles.FindAsync(id);
            if (values != null) // Null kontrolü yaparak hata riskini azaltırız
            {
                values.ArticleViewCount++;
                await _blogContext.SaveChangesAsync();
            }
        }

        public async Task<List<Comment>> CommentListWithArticleId(int id)
        {
            var comments = await _blogContext.Comments
                .Where(c => c.ArticleId == id)
                .Include(z => z.AppUser)
                .OrderByDescending(c => c.CreatedDate)
                .ToListAsync();

            return comments;
        }
    }
}
