using OnlineLibrary.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLibrary.Data.Repos
{
    public interface IGenericRepository<T> where T : AbstractEntity
    {
        IEnumerable<T> GetAll();

        T GetById(object id);

        int Insert(T obj);

        void Update(T obj);

        void Delete(object id);

        void Save();
    }
}
