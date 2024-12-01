﻿using BusinessLyaer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLyaer.Concrete
{
    public class ArticleManager : IArticleService
    {
        private readonly IArticleDal _articleDal;

        public ArticleManager(IArticleDal articleDal)
        {
            _articleDal = articleDal;
        }

        public List<Article> TArticleListWithCategoryAndAppUser()
        {
           return _articleDal.ArticleListWithCategoryAndAppUser();
        }

        public void TDelete(int id)
        {
            _articleDal.Delete(id);
        }

        public List<Article> TGetAll()
        {
          return _articleDal.GetAll();
        }

        public Article TGetById(int id)
        {
          return _articleDal.GetById(id);
        }

        public void TInsert(Article entity)
        {
           _articleDal.Insert(entity);
        }

        public void TUpdate(Article entity)
        {
            _articleDal.Update(entity);
        }
    }
}