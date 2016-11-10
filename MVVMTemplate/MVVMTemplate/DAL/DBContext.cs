using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;

namespace MVVMTemplate.DAL
{
    public partial class DBContext : DbContext
    {
        public DBContext()
            : base("")
        {

        }
    }
}
