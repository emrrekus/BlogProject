using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Context
{
    public class BlogContext : IdentityDbContext<AppUser, AppRole, int>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-S74UGVJ;initial catalog=BlogMarkedia;integrated security=true; TrustServerCertificate=true");
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);

            builder.Entity<ArticleTag>()
         .HasKey(x => new { x.ArticleId, x.TagCloudId });

            builder.Entity<ArticleTag>()
                .HasOne(z => z.Article)
                .WithMany(z => z.Tags)
                .HasForeignKey(z => z.ArticleId);

            builder.Entity<ArticleTag>()
                .HasOne(c => c.TagCloud)
                .WithMany(c => c.Articles)
                .HasForeignKey(c => c.TagCloudId);

            builder.Entity<ArticleTag>()
       .ToTable("ArticleTags");
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<SocialMedia> SocialMedias { get; set; }

        public DbSet<NewsLetter> NewsLetters { get; set; }

        public DbSet<TagCloud> Tags { get; set; }

    }
}
