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

        public void ArticleViewCountIncrease(int id)
        {
            var values = _blogContext.Articles.Find(id);
            values.ArticleViewCount++;
            _blogContext.SaveChanges();
        }
    }
}
