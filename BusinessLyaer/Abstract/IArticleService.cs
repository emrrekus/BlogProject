﻿using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLyaer.Abstract
{
    public interface IArticleService : IGenericService<Article>
    {

        List<Article> TArticleListWithCategoryAndAppUser();
        Article TArticleListWithCategoryAndAppUserByArticleId(int id);
        void TArticleViewCountIncrease(int id);

        Task<List<Comment>> TCommentListWithArticleId(int id);
    }
}
