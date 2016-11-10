using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMTemplate.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DBContext _context;

        public UnitOfWork (DBContext context)
        {

        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
