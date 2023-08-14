using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Domain.IRepository
{
    public interface ITranslationsRepository<T> where T : class
    {
        Task<List<T>> GetTranslatedDataAsync<U>(string language) where U : class;
    }
}
