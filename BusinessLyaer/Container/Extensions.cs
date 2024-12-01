using BusinessLyaer.Abstract;
using BusinessLyaer.Concrete;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLyaer.Container
{
    public static class Extensions
    {

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
      
            services.AddScoped<IArticleDal, EfArticleDal>();
            services.AddScoped<IArticleService, ArticleManager>();

            services.AddScoped<ICategoryDal, EfCategoryDal>();
            services.AddScoped<ICategoryService, CategoryManager>();

          
            services.AddScoped<ICommentDal, EfCommentDal>();
            services.AddScoped<ICommentService, CommentManager>();

            services.AddScoped<INewsLetterDal, EfNewsLetterDal>();
            services.AddScoped<INewsLetterService, NewsLetterManager>();

            services.AddScoped<ITagCloudDal, EfTagCloudDal>();
            services.AddScoped<ITagCloudService, TagCloudManager>();

            services.AddScoped<ISocialMediaDal, EfSocialMediaDal>();
            services.AddScoped<ISocialMediaService, SocialMediaManager>();

            return services;
        }
    }

    
}
