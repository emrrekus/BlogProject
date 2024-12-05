using DataAccessLayer.Abstract;
using DataAccessLayer.Context;
using DataAccessLayer.Repositories;
using EntityLayer.Concrete;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Http;
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



        public Task<List<Article>> ArticleListWithCategoryAndAppUserbyUserIdAsync(int id)
        {
            return _blogContext.Articles
                 .Where(x => x.AppUserId == id)
                 .Include(x => x.Category)
                 .Include(x => x.AppUser)
                 .Include(x => x.Tags)
                 .ThenInclude(x => x.TagCloud)
                 .ToListAsync();


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

        public async Task<Article> ArticleWithCategoryAndAppUserByArticleIdAsync(int id)
        {
            var values = await _blogContext.Articles
      .Where(x => x.ArticleId == id)
      .Include(x => x.Category)
      .Include(x => x.AppUser)
      .Include(x => x.Tags)
          .ThenInclude(t => t.TagCloud)
      .FirstOrDefaultAsync();

            return values;

        }



        public async Task ArticleViewCountIncreaseAsync(int id)
        {
            var values = await _blogContext.Articles.FindAsync(id);
            if (values != null)
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

        public async Task UpdateArticleWithTagsAsync(Article article, int[] tagId, IFormFile? CoverImage, IFormFile? MainImage)
        {

            var existingArticle = await _blogContext.Articles
        .Include(a => a.Tags)
        .FirstOrDefaultAsync(a => a.ArticleId == article.ArticleId);




            if (tagId != null)
            {
                existingArticle.Tags.Clear();
                foreach (var tag in tagId)
                {
                    existingArticle.Tags.Add(new ArticleTag
                    {
                        ArticleId = article.ArticleId,
                        TagCloudId = tag
                    });
                }
            }


            string uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/userimage");
            EnsureDirectoryExists(uploadFolder);

            if (CoverImage != null)
            {
                existingArticle.CoverImageUrl = await SaveFileAsync(CoverImage, uploadFolder);
            }

            if (MainImage != null)
            {
                existingArticle.MainImageUrl = await SaveFileAsync(MainImage, uploadFolder);
            }


            existingArticle.Title = article.Title;
            existingArticle.Detail = article.Detail;
            existingArticle.CategoryId = article.CategoryId;

            await _blogContext.SaveChangesAsync();

        }

        public async Task<string> SaveFileAsync(IFormFile file, string uploadFolder)
        {
            if (file != null && file.Length > 0)
            {

                string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);


                string filePath = Path.Combine(uploadFolder, uniqueFileName);


                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }


                return "/userimage/" + uniqueFileName;
            }

            return null;
        }

        public void EnsureDirectoryExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public async Task CreateArticle(Article article, int UserId, int[] tagId, IFormFile CoverImage, IFormFile MainImage)
        {
            string uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/userimage");
            EnsureDirectoryExists(uploadFolder);
            article.CoverImageUrl = await SaveFileAsync(CoverImage, uploadFolder);
            article.MainImageUrl = await SaveFileAsync(MainImage, uploadFolder);
            article.CreatedDate = DateTime.Now;
            article.ArticleViewCount = 0;
            article.AppUserId = UserId;

            if (article.Tags == null)
            {
                article.Tags = new List<ArticleTag>();
            }

            if (tagId != null)
            {
             
                foreach (var tag in tagId)
                {
                    article.Tags.Add(new ArticleTag
                    {
                        ArticleId = article.ArticleId,
                        TagCloudId = tag
                    });
                }
            }


            _blogContext.Articles.Add(article);
            await _blogContext.SaveChangesAsync();




        }
    }
}
