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
    public class EfCommentDal : GenericRepository<Comment>, ICommentDal
    {
        private BlogContext _blogContext;
        public EfCommentDal(BlogContext blogContext) : base(blogContext)
        {
            _blogContext = blogContext;
        }

        public Task<List<Comment>> CommentListByUserIdAsync(int id)
        {
           return _blogContext.Comments.Where(x=> x.AppUserId == id)
                .Include(x => x.Article)
                .Include(x=> x.AppUser)
                .ToListAsync();
        }
    }
}
