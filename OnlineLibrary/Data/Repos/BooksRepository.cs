using Microsoft.EntityFrameworkCore;
using OnlineLibrary.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace OnlineLibrary.Data.Repos
{
    public class BooksRepository : GenericRepository<Book>, IBookRepository
    {

        public override IEnumerable<Book> GetAll()
        {
            return table.Include(b => b.Writer)
                .Include(b => b.Genre).ToList();
        }

        public override Book GetById(object id)
        {
            return table.Include(b => b.Writer)
                .Include(b => b.Genre).Where(b => b.Id == (int)id).FirstOrDefault();
        }
    }
}
