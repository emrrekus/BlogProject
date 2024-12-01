using DataAccessLayer.Abstract;
using DataAccessLayer.Context;
using DataAccessLayer.Repositories;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfNewsLetterDal : GenericRepository<NewsLetter>, INewsLetterDal
    {
        private BlogContext _blogContext;
        public EfNewsLetterDal(BlogContext blogContext) : base(blogContext)
        {
            _blogContext = blogContext;
        }

        public bool ExistingNewsLetter(NewsLetter newsLetter)
        {
            return !_blogContext.NewsLetters.Any(x => x.Mail == newsLetter.Mail);
        }
    }
}
