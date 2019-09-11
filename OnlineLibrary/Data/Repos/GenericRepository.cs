using Microsoft.EntityFrameworkCore;
using OnlineLibrary.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLibrary.Data.Repos
{
    public class GenericRepository<T> : IGenericRepository<T> where T : AbstractEntity
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

        public virtual IEnumerable<T> GetAll()
        {
            return table.ToList();
        }

        public virtual T GetById(object id)
        {
            return table.Find(id);
        }

        public int Insert(T obj)
        {
            var res = table.Add(obj);
            return res.Entity.Id;
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
