using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLibrary.Data.Repos
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        public OnlineLibraryDBContext _context = null;
        public DbSet<T> table = null;

        public GenericRepository()
        {
            this._context = new OnlineLibraryDBContext(new DbContextOptions<OnlineLibraryDBContext>());
            table = _context.Set<T>();
        }

        public GenericRepository(OnlineLibraryDBContext _context)
        {
            this._context = _context;
            table = _context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }

        public T GetById(object id)
        {
            return table.Find(id);
        }

        public void Insert(T obj)
        {
            table.Add(obj);
        }

        public void Update(T obj)
        {
            table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }

        public void Delete(object id)
        {
            T existing = table.Find(id);
            table.Remove(existing);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
