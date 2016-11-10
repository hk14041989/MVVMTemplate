using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMTemplate.DAL
{
    public interface IUnitOfWork : IDisposable
    {
        int SaveChanges();
    }
}
