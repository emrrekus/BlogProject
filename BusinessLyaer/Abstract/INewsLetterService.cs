using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLyaer.Abstract
{
    public interface INewsLetterService : IGenericService<NewsLetter>
    {

        bool TExistingNewsLetter(NewsLetter newLetter);
    }
}
